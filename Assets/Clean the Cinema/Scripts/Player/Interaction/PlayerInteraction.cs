using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction clickAction;

    [Header ("Camera")]
    [SerializeField] private Transform camera;

    [Header ("Raycast")]
    [SerializeField] private float rayDistance;
    private IInteractable target;

    RaycastHit hit;

    [Header("Outline")]
    [SerializeField] private string outlineLayerName = "Outline";
    [SerializeField] private string defaultLayerName = "Default";
    
    
    private int outlineLayer;
    private int defaultLayer;

    private Renderer targetRenderer;


    void Awake()
    {
         playerInput = GetComponent<PlayerInput>();
        clickAction= playerInput.actions["Click"];

        outlineLayer = LayerMask.NameToLayer(outlineLayerName);
        defaultLayer = LayerMask.NameToLayer(defaultLayerName);
    }

    
    // Update is called once per frame
    void Update()
    {
        IInteractable newTarget = null;
        Renderer newTargetRenderer = null;

        if(Physics.Raycast(camera.position,camera.forward, out hit, rayDistance))
        {
            newTarget = hit.collider.gameObject.GetComponentInParent<IInteractable>();
            newTargetRenderer = hit.collider.gameObject.GetComponentInParent<Renderer>();
        } else if(targetRenderer != null && targetRenderer.gameObject.layer == outlineLayer)
        {
            targetRenderer.gameObject.layer = defaultLayer;
            targetRenderer = null; 
        }
            //Debug.Log(newTargetRenderer);

        if(newTarget != target)
        {
            target = newTarget;
            targetRenderer = newTargetRenderer;
           
            if(target != null && targetRenderer.gameObject.layer == outlineLayer)
            {
            targetRenderer.gameObject.layer = defaultLayer;
            }

            if (target != null && targetRenderer != null)
            {
                targetRenderer.gameObject.layer = outlineLayer;
            } 

            if(targetRenderer == null)
            {
                
            }
        }

        //Debug.Log(targetRenderer);

        if (clickAction.WasPressedThisFrame() && target != null)
        {
            target.Interact();
        }

        
    }

     void OnDrawGizmos()
    {
        Gizmos.DrawRay(camera.position,camera.forward*rayDistance);
    }
}
