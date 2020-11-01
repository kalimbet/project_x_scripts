using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region SingletonPattern

    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogWarning("More than one instance of EquipmentManager is found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    public Equipment[] currentEquipment;
    public int numSlots = 6;
    
    InventoryManager inventory;
    int lastItemIndex = 0;

    void Start()
    {
        inventory = InventoryManager.instance;
        currentEquipment = new Equipment[numSlots];
    }
    
    public bool Equip (Equipment newItem)
    {
        if(lastItemIndex >= numSlots)
        {
            Debug.Log("Inventory is full. Cannot add more items");
            return false;
        }

        Equipment oldItem = null;

        for(int i = 0; i < currentEquipment.Length; i++)
        {
            if (currentEquipment[i] == null)
            {
                currentEquipment[i] = newItem;
                break;
            }
                
        }  

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        lastItemIndex++;

        return true;
    }

    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            lastItemIndex--;
        }
    }
}
