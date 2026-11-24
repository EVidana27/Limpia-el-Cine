using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerBag playerBag;
    [SerializeField] private PlayerAbilities playerAbilities;

    [SerializeField] private int itemValue;

    void Awake()
    {
        playerBag = FindAnyObjectByType<PlayerBag>();
        playerAbilities = FindAnyObjectByType<PlayerAbilities>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Picked up this item");
        playerBag.PickupItem(playerAbilities.PickupAmount);
        MoneyManager.instance.AccumulateMoney(itemValue);
        GameManager.instance.AddToProgress();
        Destroy(gameObject);
    }


    private void CheckAround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,playerAbilities.CheckRadius);
        
        foreach(Collider col in hitColliders)
        {
            col.GetComponent<PickupItem>();
        }
    }
}
