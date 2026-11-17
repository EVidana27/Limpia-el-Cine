using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule.Util;

public class SelectionOutlineURPRenderFeature : ScriptableRendererFeature
{
    [SerializeField] SelectionOutlineURPRenderFeatureSettings settings;
    SelectionOutlineURPRenderFeaturePass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new SelectionOutlineURPRenderFeaturePass(settings);

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

        // You can request URP color texture and depth buffer as inputs by uncommenting the line below,
        // URP will ensure copies of these resources are available for sampling before executing the render pass.
        // Only uncomment it if necessary, it will have a performance impact, especially on mobiles and other TBDR GPUs where it will break render passes.
        //m_ScriptablePass.ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth);

        // You can request URP to render to an intermediate texture by uncommenting the line below.
        // Use this option for passes that do not support rendering directly to the backbuffer.
        // Only uncomment it if necessary, it will have a performance impact, especially on mobiles and other TBDR GPUs where it will break render passes.
        //m_ScriptablePass.requiresIntermediateTexture = true;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType != CameraType.Game || renderingData.cameraData.renderType != CameraRenderType.Base) return;
        renderer.EnqueuePass(m_ScriptablePass);
    }

    // Use this class to pass around settings from the feature to the pass
    [Serializable]
    public class SelectionOutlineURPRenderFeatureSettings
    {
        public LayerMask outlinedLayer;
        public Material maskMaterial;
        public Material outlineMaterial;
    }

    class SelectionOutlineURPRenderFeaturePass : ScriptableRenderPass
    {
        readonly SelectionOutlineURPRenderFeatureSettings settings;

        static readonly List<ShaderTagId> shaderTags = new()
        {
            new ShaderTagId("UniversalForward"),
            new ShaderTagId("UniversalForwardOnly"),
            new ShaderTagId("SRPDefaultUnlit")
        };


        public SelectionOutlineURPRenderFeaturePass(SelectionOutlineURPRenderFeatureSettings settings)
        {
            this.settings = settings;
        }

        // This class stores the data needed by the RenderGraph pass.
        // It is passed as a parameter to the delegate function that executes the RenderGraph pass.
        private class PassData
        {
            public RendererListHandle rendererList;

        }

        // This static method is passed as the RenderFunc delegate to the RenderGraph render pass.
        // It is used to execute draw commands.
        static void ExecutePass(PassData data, RasterGraphContext context)
        {
            context.cmd.DrawRendererList(data.rendererList);
        }

        // RecordRenderGraph is where the RenderGraph handle can be accessed, through which render passes can be added to the graph.
        // FrameData is a context container through which URP resources can be accessed and managed.
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
            UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
            UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
            UniversalLightData lightData = frameData.Get<UniversalLightData>();
            const string passName = "Outline Custom Pass";

            TextureDesc maskDescriptor = renderGraph.GetTextureDesc(resourceData.activeDepthTexture);
            maskDescriptor.name = "Selection Mask";
            maskDescriptor.format = GraphicsFormat.R8G8B8A8_UNorm;
            maskDescriptor.clearBuffer = true;
            maskDescriptor.clearColor = Color.black;
            TextureHandle maskTexture = renderGraph.CreateTexture(maskDescriptor);

            DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(shaderTags, renderingData, cameraData, lightData, cameraData.defaultOpaqueSortFlags);
            drawingSettings.overrideMaterial = settings.maskMaterial;
            drawingSettings.overrideMaterialPassIndex = 0;

            FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.opaque, settings.outlinedLayer);
            RendererListParams rendererListParams = new RendererListParams(renderingData.cullResults, drawingSettings, filteringSettings);


            // This adds a raster render pass to the graph, specifying the name and the data type that will be passed to the ExecutePass function.
            using (var builder = renderGraph.AddRasterRenderPass<PassData>(passName, out var passData))
            {
                passData.rendererList = renderGraph.CreateRendererList(rendererListParams);
                builder.UseRendererList(passData.rendererList);

                builder.AllowPassCulling(false);
                builder.SetRenderAttachment(maskTexture, 0, AccessFlags.Write);
                builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Read);

                builder.SetRenderFunc<PassData>(ExecutePass);
            }

            var properties = new MaterialPropertyBlock();
            properties.SetVector("_OutlineTexelSize", new Vector4(1f / maskDescriptor.width, 1f / maskDescriptor.height, 0f, 0f));

            var blitParameters = new RenderGraphUtils.BlitMaterialParameters(maskTexture, resourceData.activeColorTexture, settings.outlineMaterial, 0)
            {
                propertyBlock = properties
            };
            renderGraph.AddBlitPass(blitParameters, "Selection Outline - Composite");
        }
    }
}
