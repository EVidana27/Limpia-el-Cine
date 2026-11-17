using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Player Components")]
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CapsuleCollider playerCollider;

    [Header ("Camera")]
    [SerializeField] private Transform cameraTargetTransform;

    [Header ("Player Stats")]
    [SerializeField] private float playerSpeed; 

    [SerializeField] private float sprintMultiplier;
    private float baseSpeed;

    [Header ("Player Flags")]
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isSprinting;

    [Header ("Input Actions")]
    private InputAction movementAction;
    private InputAction crouchAction;
    private InputAction sprintAction;

    [Header ("Crouch")]
    private float baseColliderHeight;
    private float baseColliderCenter;
     private float baseCameraHeight;
    [SerializeField] private float crouchedColliderCenter;
    [SerializeField] private float crouchedColliderHeight;
    [SerializeField] private float crouchedCameraHeight;


    [Header ("Ground Check")]
    [SerializeField] private float checkSize;
    [SerializeField] private Transform checkOrigin;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 movementDirection;

    void Awake()
    {
         playerRB = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerCollider = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
       
        AssignActions();

        baseSpeed = playerSpeed;

        baseColliderCenter = playerCollider.center.y;
        baseColliderHeight = playerCollider.height;

        baseCameraHeight = cameraTargetTransform.localPosition.y;

    }

    private void AssignActions()
    {
        movementAction = playerInput.actions["Movement"];
        sprintAction = playerInput.actions["Sprint"];
        crouchAction = playerInput.actions["Crouch"];
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = movementAction.ReadValue<Vector2>();
        if (IsGrounded())
        {
        isSprinting = sprintAction.IsPressed();
        isCrouching = crouchAction.IsPressed();
        }

        ManageSpeed();
        PlayerCrouch();

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y=0f;
        right.y=0f;

        Vector3 FPMovementDirection = forward * movementDirection.y + right * movementDirection.x;
        FPMovementDirection.Normalize();

        playerRB.linearVelocity = new Vector3(FPMovementDirection.x*playerSpeed,playerRB.linearVelocity.y,FPMovementDirection.z*playerSpeed);
    }

    private void ManageSpeed()
    {
        if (isCrouching)
        {
            playerSpeed = baseSpeed/2;
        } else if(isSprinting){

            playerSpeed = baseSpeed * sprintMultiplier;
        } else
        {
            playerSpeed = baseSpeed;
        }
    }

    private void PlayerCrouch()
    {
        if (isCrouching)
        {
            playerCollider.height = crouchedColliderHeight;
            playerCollider.center = new Vector3(playerCollider.center.x,crouchedColliderCenter,playerCollider.center.z);
            cameraTargetTransform.localPosition = new Vector3(cameraTargetTransform.localPosition.x,crouchedCameraHeight,cameraTargetTransform.localPosition.z);
        } else
        {
            playerCollider.height = baseColliderHeight;
            playerCollider.center = new Vector3(playerCollider.center.x,baseColliderCenter,playerCollider.center.z);
            cameraTargetTransform.localPosition = new Vector3(cameraTargetTransform.localPosition.x,baseCameraHeight,cameraTargetTransform.localPosition.z);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(checkOrigin.position,checkSize,groundLayer,QueryTriggerInteraction.Ignore);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkOrigin.position,checkSize);
    }
}
