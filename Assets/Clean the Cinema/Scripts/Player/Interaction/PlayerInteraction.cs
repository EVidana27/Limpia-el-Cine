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

    void Awake()
    {
         playerInput = GetComponent<PlayerInput>();
        clickAction= playerInput.actions["Click"];
    }


    // Update is called once per frame
    void Update()
    {
        IInteractable newTarget = null;

        if(Physics.Raycast(camera.position,camera.forward, out hit, rayDistance))
        {
            newTarget = hit.collider.gameObject.GetComponentInParent<IInteractable>();
        }

        if(newTarget != target)
        {
            target = newTarget;
        }

        if(clickAction.WasPressedThisFrame() && target != null)
        {
            target.Interact();
        }

        
    }

     void OnDrawGizmos()
    {
        Gizmos.DrawRay(camera.position,camera.forward*rayDistance);
    }
}
