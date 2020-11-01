using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LKnightData : MonoBehaviour
{
    public static LSMain.SaveKnightData ldData = new LSMain.SaveKnightData();
    public bool load = false;

    void Update()
    {
        if (load)
        {
            LoadKnightDataFromFile();
            load = false;
        }
    }

    public static void LoadKnightDataFromFile()
    {
        if (File.Exists(LSMain.knightDataPath))
        {
            ldData = JsonUtility.FromJson<LSMain.SaveKnightData>(File.ReadAllText(LSMain.knightDataPath));
            PutKnightDataToGame();
        }
        else
        {
            Debug.Log("Knight Save File doesn't exist");
        }
    }

    public static void PutKnightDataToGame()
    {
        KnightMain.level = ldData.knightLevel;
        KnightMain.experience = ldData.knightExperience;
        KnightMain.kills = ldData.knightKills;
        KnightMain.death = ldData.knightDeath;
        KnightMain.takenDamage = ldData.knightTakenDamage;
        KnightMain.givenDamage = ldData.knightGivenDamage;
    }
}
