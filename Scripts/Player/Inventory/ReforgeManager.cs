using System.Collections.Generic;
using UnityEngine;

public class ReforgeManager : MonoBehaviour
{
    #region singleton
    public static ReforgeManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogWarning("More than one instance of ReforgeManager is found!");
            return;
        }

        instance = this;
    }
    #endregion

    public ItemRarityPartitioner partitioner;

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public Equipment[] itemsToReforge;
    public int numOfReforgeSlots = 5; // set this as number of ALL reforge slots (fodder gear + the craft result slot) in inspector
    public int maxReforgeTier = 2; // see equipment.rarity for tiers;

    [HideInInspector] public bool reforgeMode = false;

    private InventoryManager inventory;

    private int lastItemIndex = 0;

    private void Start()
    {
        itemsToReforge = new Equipment[numOfReforgeSlots];
        partitioner.FillArraysFromItemDB(); // Separate items by tiers once on startup
        inventory = InventoryManager.instance;
    }

    public void AddItemToGrid(Equipment item)
    {
        if(lastItemIndex >= numOfReforgeSlots - 1) // it is set to -1 in order to exclude the final (craft result) slot
        {
            Debug.Log("No more slots for equipment available!");
            return;
        }

        for(int i = 0; i < itemsToReforge.Length; i++)
        {
            if(itemsToReforge[i] == null)
            {
                itemsToReforge[i] = item;
                inventory.RemoveItem(item);
                break;
            }
        }
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        lastItemIndex++;
    }

    public void RemoveItemFromGrid(int slotIndex)
    {
        if(itemsToReforge[slotIndex] != null)
        {
            Equipment item = itemsToReforge[slotIndex];
            inventory.AddItem(item);

            itemsToReforge[slotIndex] = null;

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            
            if(slotIndex != itemsToReforge.Length - 1)  // do not decrease item index if we take out the crafting result
                lastItemIndex--;
        }
    }

    public void ClearGridSlot(int slotIndex)
    {
        if (itemsToReforge[slotIndex] != null)
        {
            itemsToReforge[slotIndex] = null;
            lastItemIndex--;
        }
    }

    public void ForceRemoveAllItems()
    {
        for (int i = 0; i < itemsToReforge.Length; i++)
        {
            RemoveItemFromGrid(i);
        }
    }

    public void UpgradeItem()
    {
        bool isReadyToReforge = CheckIfReadyForReforge();

        if (isReadyToReforge)
        {
            var equipmentToSelect = partitioner.fullDB[(int)itemsToReforge[0].rarity + 1]; // it is set +1, because we need to craft a rarity above
            int index = Random.Range(0, equipmentToSelect.Count);

            for(int i = 0; i < itemsToReforge.Length; i++)
            {
                ClearGridSlot(i);
            }

            itemsToReforge[itemsToReforge.Length - 1] = (Equipment) equipmentToSelect[index];
        } else
        {
            return; // DO NOTHING if one or more item checks fail;
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    private bool CheckIfReadyForReforge()
    {
        int comparisonTier = -1; // -1 is a dummy value to be replaced in next line

        if (itemsToReforge[0] != null)
            comparisonTier = (int)itemsToReforge[0].rarity; // use first item for tier comparison

        for (int i = 0; i < numOfReforgeSlots - 1 ; i++) // we set numOfReforgeSlots - 1, because we exclude the final (forge result) slot
        {
            Equipment currentItem = itemsToReforge[i];

            if (currentItem == null) // check if all slots are filled
            {
                Debug.LogWarning("Not enough items for reforging!");
                return false;
            }

            if ((int)currentItem.rarity >= maxReforgeTier) // check if all items are not above max reforge tier
            {
                Debug.LogWarning("One or more items is on par or above max reforge tier!");
                return false;
            }

            if((int)currentItem.rarity != comparisonTier) // check if all items are same tier
            {
                Debug.LogWarning("One or more items are not from same tier!");
                return false;
            }
        }

        if(itemsToReforge[itemsToReforge.Length - 1] != null) // check if craft result slot is not empty
        {
            Debug.LogWarning("Please remove the previous crafting result before reforging a new item!");
            return false;
        }

        return true;
    }
}
