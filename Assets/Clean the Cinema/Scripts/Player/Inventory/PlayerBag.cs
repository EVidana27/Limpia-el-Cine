using System;
using TMPro;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    [Header ("Bag Settings")]
    [SerializeField] private int bagCapacity;


    [SerializeField] private int currentBagAmount;


    [SerializeField] private TMP_Text capacityTest;
    [SerializeField] private UpgradeMenu upgradeMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        capacityTest.gameObject.SetActive(!upgradeMenu.IsMenuOpen);
        if (capacityTest.gameObject.activeSelf)
        {
            capacityTest.text = currentBagAmount.ToString() + " / " + bagCapacity.ToString();
        }
    }

    public void SetNewBagCapacity(int newCapacity)
    {
        if(newCapacity < 0) return;

        bagCapacity = newCapacity;
    }

    public void PickupItem(int amount)
    {
        currentBagAmount = Mathf.Clamp(currentBagAmount,0,bagCapacity);

        if(currentBagAmount <= bagCapacity)
        {
            currentBagAmount+=amount;
        }
    }

    public void DepositObjects()
    {
        currentBagAmount = 0;
    }
}
