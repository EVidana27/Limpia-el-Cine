using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header ("Camera Settings")]
    [SerializeField] private float mouseSensitivity;
    
    [Header ("Player")]
    [SerializeField] private Transform playerTransform;

    [Header ("Input Actions")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction lookAction;

    [Header ("Rotation")]
    [SerializeField] private float verticalLimit;
    private float verticalRotation;
    private float horizontalRotation;
    private Vector2 lookDirection; 
    void Start()
    {
        lookAction = playerInput.actions["Look"];

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = lookAction.ReadValue<Vector2>();

        float mouseX = lookDirection.x * mouseSensitivity;
        float mouseY = lookDirection.y * mouseSensitivity;

        verticalRotation -=mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation,-verticalLimit,verticalLimit);

        transform.localRotation = Quaternion.Euler(verticalRotation,0f,0f);
        horizontalRotation+=mouseX;
    }

    private void FixedUpdate()
    {
        playerTransform.rotation = Quaternion.Euler(0f,horizontalRotation,0f);
    }
}
