using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TextMeshProUGUI itemCountText;

    public bool enableInventoryButton = false; // enable item remove button in inventory

    Item item;

    public void AddItem(Item newItem, int count)
    {
        item = newItem;

        if(icon != null && itemCountText != null) { 
            icon.sprite = item.icon;
            icon.enabled = true;
            if(enableInventoryButton)
                removeButton.interactable = true;

            if (!itemCountText.enabled)
            {
                itemCountText.enabled = true;
            }

            itemCountText.text = count.ToString();
        }

    }

    public void ClearSlot()
    {
        item = null;
        if (icon != null && itemCountText != null)
        {
            icon.sprite = null;
            icon.enabled = false;
            if(enableInventoryButton)
                removeButton.interactable = false;

            itemCountText.enabled = false;
        }
    }

    public void OnRemoveButton()
    {
        InventoryManager.instance.RemoveItem(item);
    }

    public void UseItem()
    {
        if (ReforgeManager.instance.reforgeMode)
        {
            ReforgeManager.instance.AddItemToGrid((Equipment) item);
            //InventoryManager.instance.RemoveItem(item);
            return;
        }

        if (item != null)
        {
            item.Use();
        }

    }

}
