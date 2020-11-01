using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRewards : MonoBehaviour
{
    public static void ExperienceReward(GameObject enemy)
    {

        if (enemy.GetComponent<EnemyLightMain>() != null)
        {
            KnightMain.addExperience(enemy.GetComponent<EnemyLightMain>().experience);
        }
        else
        {
            Debug.Log("Error");
        }
    }

}
