using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LSMain : MonoBehaviour
{
    public static string knightDataPath;
    public static string InvDataPath;
    public bool isPhone = false;
    public string knightDataName = "KnightData";
    public string inventoryDataName = "InvData";

    public bool removeSaveData = false;
    
    private void Awake()
    {
        if (isPhone)
        {
            knightDataPath = Path.Combine(Application.persistentDataPath, knightDataName + ".json");
            InvDataPath = Path.Combine(Application.persistentDataPath, inventoryDataName + ".json");
        }
        else
        {
            knightDataPath = Path.Combine(Application.dataPath, "Saves/" + knightDataName + ".json");
            InvDataPath = Path.Combine(Application.dataPath, "Saves/" + inventoryDataName + ".json");
        }

        if (removeSaveData) // remove save data on start
        {
            if(File.Exists(knightDataPath))
                File.Delete(knightDataPath);

            if(File.Exists(InvDataPath))
                File.Delete(InvDataPath);
        }
    }

    [SerializeField]
    public class SaveKnightData
    {
        public int knightLevel;
        public float knightExperience;
        public int knightKills;
        public int knightDeath;
        public float knightTakenDamage;
        public float knightGivenDamage;
    }
    
    [SerializeField]
    public class SaveInvData
    {
        public int[] equipmentIds;
        public int[] itemIds;
        public int[] itemCountList;
    }
}
