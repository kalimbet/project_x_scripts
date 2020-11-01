using UnityEngine;

public class ItemPickup : InteractableItem
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        if (InventoryManager.instance.AddItem(item))     // destroy object on succesful pickup
            Destroy(gameObject);
    }
}
