using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SKnightData : MonoBehaviour
{
    public static LSMain.SaveKnightData svData = new LSMain.SaveKnightData();
    public bool save = false;

    private void Update()
    {
        if (save)
        {
            SaveKnightDataToFile();
            save = false;
        }
    }

    public static void SaveKnightDataToFile()
    {
        TakeKnightDataFromGame();
        File.WriteAllText(LSMain.knightDataPath, JsonUtility.ToJson(svData));
    }

    public static void TakeKnightDataFromGame()
    {
        svData.knightLevel = KnightMain.level;
        svData.knightExperience = KnightMain.experience;
        svData.knightKills = KnightMain.kills;
        svData.knightDeath = KnightMain.death;
        svData.knightTakenDamage = KnightMain.takenDamage;
        svData.knightGivenDamage = KnightMain.givenDamage;
    }
}
