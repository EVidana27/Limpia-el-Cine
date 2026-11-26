using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    [Header ("Pickup ")]
    [SerializeField] private int pickupAmount;
    public int PickupAmount => pickupAmount;


    [Header ("Pickup Sphere")]
    [SerializeField] private float checkRadius;
    public float CheckRadius=>checkRadius;

    [Header("Deposit")]
    [SerializeField] private int depositAmount;
    [SerializeField] private bool hasDepositAbility;

    [Header("Magnet")]
    [SerializeField] private int magnetAmount;
    [SerializeField] private bool hasMagnetAbility;

    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction abilityOneAction;
    private InputAction abilityTwoAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        abilityOneAction = playerInput.actions["Ability_1"];
        abilityTwoAction = playerInput.actions["Ability_2"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradePickupAmount(int newAmount)
    {
        pickupAmount = newAmount;
    }

    public void UpgradePickupRange(float newRange)
    {
        checkRadius = newRange;
    }

    public void UpgradeMagnetAmount(int newAmount)
    {
        if (!hasMagnetAbility) return;

        magnetAmount = newAmount;
    }

    public void UpgradeDepositAmount(int newAmount)
    {
        if(!hasDepositAbility) return;
        
        depositAmount = newAmount;
    }

    public void UnlockMagnet()
    {
        hasMagnetAbility = true;
    }

    public void UnlockDeposit()
    {
        hasDepositAbility = true;
    }
}
