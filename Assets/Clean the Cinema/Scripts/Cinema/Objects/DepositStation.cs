using UnityEngine;

public class DepositStation : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerBag playerBag;

    public void Interact()
    {
        playerBag.DepositObjects();
        MoneyManager.instance.AddMoney();
        GameManager.instance.UpdateProgress();
        Debug.Log("Deposited");
    }
}
