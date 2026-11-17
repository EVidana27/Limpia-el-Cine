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
    [SerializeField] private string outlineLayerName;
    [SerializeField] private LayerMask outlinedLayer;
    [SerializeField] private LayerMask defaultLayer;

    private Renderer targetRenderer;


    void Awake()
    {
         playerInput = GetComponent<PlayerInput>();
        clickAction= playerInput.actions["Click"];

        outlinedLayer = LayerMask.NameToLayer(outlineLayerName);
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
        }
            Debug.Log(newTargetRenderer);

        if(newTarget != target)
        {
            target = newTarget;
            targetRenderer = newTargetRenderer;

            //targetRenderer.gameObject.layer = LayerMask.NameToLayer("Not Outline");

            if (target != null && targetRenderer != null)
            {
                targetRenderer.gameObject.layer = LayerMask.NameToLayer("Outline");
            } 
        }

        Debug.Log(targetRenderer);

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
