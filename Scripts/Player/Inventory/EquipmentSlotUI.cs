using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemSlotName; // slot name or equipped item?

    public int SlotNumber = 0; // TODO: change to a different counting method.

    //public EquipmentSlot equipSlot;

    Equipment equipment;

    public void EquipItem(Equipment equip)
    {
        equipment = equip;
        if (icon != null)
        {
            icon.sprite = equip.icon;
            icon.enabled = true;
            itemSlotName.enabled = false;
        }

    }

    public void UnequipItem()
    {
        // make sure to add item back to the inventory
        if (equipment != null)
        {
            Debug.Log("Unequipping " + equipment.name);
            EquipmentManager.instance.Unequip(SlotNumber);
        } 

        equipment = null;
        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
            itemSlotName.enabled = true;
        }

    }

}
