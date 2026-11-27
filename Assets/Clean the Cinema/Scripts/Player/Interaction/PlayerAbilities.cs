using System.Collections.Generic;
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

    private List<SkillTree> unlockedSkills = new();
    [SerializeField] private SkillTree skillTreeData;

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

    private void MagnetAbilitiy()
    {
        if (!hasMagnetAbility) return;

        



    }

    private bool TryToUnlockAbility(SkillTree skill)
    {
        if(unlockedSkills.Contains(skill) || MoneyManager.instance.CurrentMoney < skill.moneyCost) return false;

        if (skill.prerequisites != null) return false;

        return false;
        
    }

    
}
