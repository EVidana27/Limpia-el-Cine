using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeMenu : MonoBehaviour
{
    [Header ("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction openMenuAction;

    [Header("Flags")]
    [SerializeField] private bool isMenuOpen;

    [Header ("Game Object")]
    [SerializeField] private GameObject menuPanel;
    

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
        openMenuAction = playerInput.actions["UpgradeMenu"];
    }

    // Update is called once per frame
    void Update()
    {
        if (openMenuAction.WasPressedThisFrame())
        {
            isMenuOpen = !isMenuOpen;
            menuPanel.SetActive(isMenuOpen);

            if (isMenuOpen)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameManager.instance.ChangeGameState(GameState.Waiting);
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameManager.instance.ChangeGameState(GameState.Playing);
            }
        }

        
    }
}
