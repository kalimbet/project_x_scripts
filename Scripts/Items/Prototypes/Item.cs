using UnityEngine;

// Default item blueprint to create items from

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description = "Item Description";
    public Sprite icon = null;

    public bool isStackable = true;
    public int stackLimitTODO = 99; //TODO

    [HideInInspector] public bool isDefaultItem = false; // Deprecated mechanic

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        InventoryManager.instance.RemoveItem(this);
    }

}
