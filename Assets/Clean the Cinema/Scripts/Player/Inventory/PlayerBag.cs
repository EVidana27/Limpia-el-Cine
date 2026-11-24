using System;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    [Header ("Bag Settings")]
    [SerializeField] private int bagCapacity;


    [SerializeField] private int currentBagAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
