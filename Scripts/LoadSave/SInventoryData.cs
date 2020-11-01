using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SInventoryData : MonoBehaviour
{
    public static LSMain.SaveInvData svData2 = new LSMain.SaveInvData();    
    public bool save = false;

    private void Update()
    {
        if (save)
        {
            saveInvDataToFile();
            save = false;
        }
    }

    public static void saveInvDataToFile()
    {
        TakeDataFromGame();
        File.WriteAllText(LSMain.InvDataPath, JsonUtility.ToJson(svData2));        
    }

    public static void TakeDataFromGame()
    {
        int[] saveEquipment = new int[EquipmentManager.instance.currentEquipment.Length];
        int[] saveItems = new int[InventoryManager.instance.items.Count];

        Equipment[] cEquip = EquipmentManager.instance.currentEquipment;
        Item[] cItem = InventoryManager.instance.items.ToArray();

        // If slot is empty, set itemID to -1

        for (int i = 0; i < cEquip.Length; i++)
            if (cEquip[i] != null)
                saveEquipment[i] = ItemDatabase.Instance.GetID[cEquip[i]];
            else
                saveEquipment[i] = -1;

        for (int i = 0; i < cItem.Length; i++)
            if (cItem[i] != null)
                saveItems[i] = ItemDatabase.Instance.GetID[cItem[i]];
            else
                saveItems[i] = -1;

        svData2.itemCountList = InventoryManager.instance.itemCountList.ToArray();
        svData2.itemIds = saveItems;
        svData2.equipmentIds = saveEquipment;
    }

}
