using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReforgeUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public GameObject reforgeUI;

    ReforgeManager reforgeManager;

    ReforgeSlotUI[] rSlots;

    bool reforgeMode = false;
    
    private void Start()
    {
        rSlots = GetComponentsInChildren<ReforgeSlotUI>(includeInactive:true);
        reforgeManager = ReforgeManager.instance;
        reforgeManager.onItemChangedCallback += UpdateUI;
    }

    public void OnToggleReforgeMode() // enable\disable reforge UI
    {
        if (!reforgeMode && inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(true);
            reforgeUI.SetActive(true);
            equipmentUI.SetActive(false);
            reforgeMode = true;
            reforgeManager.reforgeMode = reforgeMode;
        } else if (!inventoryUI.activeSelf)
        {
            // inventory screen is not open, do nothing...
        } else
        {
            inventoryUI.SetActive(true);
            reforgeUI.SetActive(false);
            equipmentUI.SetActive(true);
            reforgeMode = false;
            reforgeManager.reforgeMode = reforgeMode;
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < reforgeManager.itemsToReforge.Length; i++)
        {
            Equipment currItem = reforgeManager.itemsToReforge[i];

            if (currItem != null)
                rSlots[i].AddItem(currItem);
            else
                rSlots[i].RemoveItem();
        }
    }
}
