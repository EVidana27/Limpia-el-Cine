using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    [SerializeField] private int currentMoney;
    [SerializeField] private int accumulatedMoney;

    private void Awake()
    {
        instance = this;
    }


    public void AccumulateMoney(int amount)
    {
        accumulatedMoney += amount;
    }

    public void AddMoney()
    {
        currentMoney += accumulatedMoney; 
        accumulatedMoney = 0;
    }
}
