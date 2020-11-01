using System.Collections.Generic;
using UnityEngine;

// INVENTORY MANAGER
public class InventoryManager : MonoBehaviour
{

    #region SingletonPattern

    public static InventoryManager instance;
    private void Awake() // defines singleton pattern
    {
        if (instance != null)
        {
            //Debug.LogWarning("More than one instance of inventory is found!");
            return;
        }

        instance = this;
    }

    #endregion

    public List<Item> items = new List<Item>();
    [HideInInspector] public List<int> itemCountList = new List<int>();

    public int inventorySpace = 20; // available slots in the inventory.

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public bool AddItem (Item item)
    {
        //int newValueOfStack = item.stackLimit + 1;


            if (items.Count >= inventorySpace)
            {
                Debug.Log("Not enough inventory space");
                return false;
            }

            //if (item.stackLimit < newValueOfStack)
            //{
            //    Debug.Log("Maximum number of " + item.name + " reached.");
            //    return false;
            //}

            if (items.Contains(item) && item.isStackable)
            {
                int index = items.IndexOf(item);
                itemCountList[index] += 1;
            } else
            {
                items.Add(item);
                itemCountList.Add(1);
            }

            
            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        
        return true;
    }

    public void RemoveItem (Item item)
    {
        int index = items.IndexOf(item);
        int itemCount = itemCountList[index];

        if(itemCount > 1)
        {
            itemCountList[index] -= 1;

        } else
        {
            items.Remove(item);
            itemCountList.RemoveAt(index);
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
