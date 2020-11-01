using UnityEngine;
using UnityEngine.UI;

public class ReforgeSlotUI : MonoBehaviour
{
    public Image icon;
    public int slotNumber = 0;

    Equipment equipment;

    public void AddItem(Equipment equip)
    {
        equipment = equip;
        icon.sprite = equip.icon;
        icon.enabled = true;
    }

    public void RemoveItem()
    {
        if(equipment != null)
            ReforgeManager.instance.RemoveItemFromGrid(slotNumber);

        equipment = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
