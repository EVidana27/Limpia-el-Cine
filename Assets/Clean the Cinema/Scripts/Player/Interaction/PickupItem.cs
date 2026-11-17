using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    
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
    }
}
