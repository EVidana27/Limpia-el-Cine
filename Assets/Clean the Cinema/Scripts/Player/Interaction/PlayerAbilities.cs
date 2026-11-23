using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header ("Pickup ")]
    [SerializeField] private int pickupAmount;
    public int PickupAmount => pickupAmount;


    [Header ("Pickup Sphere")]
    [SerializeField] private float checkRadius;
    public float CheckRadius=>checkRadius; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradePickupAmount(int newAmount)
    {
        pickupAmount = newAmount;
    }
}
