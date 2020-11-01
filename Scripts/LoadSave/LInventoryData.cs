using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LInventoryData : MonoBehaviour
{
    public static LSMain.SaveInvData ldData2 = new LSMain.SaveInvData();
    public bool load = false;

    void Update()
    {
        if (load)
        {
            LoadInvDataFromFile();
            load = false;
        }
    }

    public static void LoadInvDataFromFile()
    {
        if (File.Exists(LSMain.InvDataPath))
        {
            ldData2 = JsonUtility.FromJson<LSMain.SaveInvData>(File.ReadAllText(LSMain.InvDataPath));
            PutKnightDataToGame(true); // TODO: Change loading to a different scene. Then change this to false.
        }
        else
        {
            Debug.Log("Inventory Save File doesn't exist");
        }
    }

    public static void PutKnightDataToGame(bool overwriteItemList)
    {
        int[] loadEquipment = ldData2.equipmentIds;
        int[] loadItems = ldData2.itemIds;
        int[] loadItemCount = ldData2.itemCountList;

        var equipInstance = EquipmentManager.instance;
        var invInstance = InventoryManager.instance;

        if (overwriteItemList)
        {
            invInstance.items = new List<Item>();
            invInstance.itemCountList = new List<int>();
        }

        for (int i = 0; i < loadEquipment.Length; i++)
        {
            if (loadEquipment[i] == -1)
                continue;
            equipInstance.currentEquipment[i] = (Equipment)ItemDatabase.Instance.GetItem[loadEquipment[i]];
        }

        for(int i = 0; i < loadItems.Length; i++)
        {
            invInstance.items.Add(ItemDatabase.Instance.GetItem[loadItems[i]]);
            invInstance.itemCountList.Add(loadItemCount[i]);
        }
    }
}
