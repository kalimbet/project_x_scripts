using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Button toggleInventoryButton;
    public GameObject inventoryUI;
    public GameObject equipmentUI;

    public bool forceUpdateOnStart = false;

    bool initialized = false;

    InventoryManager inventory;
    EquipmentManager equipment;

   // These are same for each category; they handle the UI
    InventorySlot[] slots;
    EquipmentSlotUI[] eSlots;

    void Start()
    {
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateUI;

        equipment = EquipmentManager.instance;

        slots = GetComponentsInChildren<InventorySlot>();
        eSlots = GetComponentsInChildren<EquipmentSlotUI>();

        initialized = true;

        if (forceUpdateOnStart)
            ForceRedrawInventory();
    }

    void UpdateUI()
    {
        if (!initialized) {
            Start(); //crutch
        }

        for (int i = 0; i < equipment.currentEquipment.Length; i++)
        {
            Equipment currentItem = equipment.currentEquipment[i];

            if (currentItem != null)
            {
                eSlots[i].EquipItem(currentItem);
            }
        }


        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i], inventory.itemCountList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void OnToggleInventoryButton()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        equipmentUI.SetActive(!equipmentUI.activeSelf);    
    }

    public void ForceRedrawInventory() {
        UpdateUI();
    }
}
