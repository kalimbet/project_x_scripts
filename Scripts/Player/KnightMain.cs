using System.IO;
using UnityEngine;

public class KnightMain : MonoBehaviour
{
    public static bool blockStatus = false;
    public static bool movementStatus = true;
    public static bool attackStatus = false;
    public static bool dodgeStatus = false;

    public float thisHealth = 100f;
    public float thisDefence = 2f;
    public float thisMaxDefenceRating = 99f;
    public float thisHpRegeneration = 3f;

    
    public float thisLightAttackDamage = 15f;
    public float thisMediumAttackDamage = 25f;
    public float thisHeavyAttackDamage = 50f;
    public int thisLevel = 0;
    public int thisKills = 0;
    public float thisExperience = 0;
    public int thisDeath = 0;
    public float thisTakenDamage = 0;
    public float thisGivenDamage = 0;

    public static float health;
    public static float defence;
    public static float maxDefenceRating;
    public static float hpRegeneration;
    public static int level;
    public static float experience;
    public static int death;
    public static int kills;
    public static float takenDamage;
    public static float givenDamage;
    public static float lightAttackDamage;
    public static float mediumAttackDamage;
    public static float heavyAttackDamage;

    private void Awake()
    {
        CheckDataForLoad();

        health = thisHealth;
        defence = thisDefence;
        maxDefenceRating = thisMaxDefenceRating;
        hpRegeneration = thisHpRegeneration;
        lightAttackDamage = thisLightAttackDamage;
        mediumAttackDamage = thisMediumAttackDamage;
        heavyAttackDamage = thisHeavyAttackDamage;

        recalculateStats();

    }


    // Main technical methods
    public void setMoveStatusTrue()
    {
        movementStatus = true;
    }

    public void setMoveStatusFalse()
    {
        movementStatus = false;
    }

    public static void addLevel(int value)
    {
        level += value;
    }

    public static void addExperience(float value)
    {
        experience += value;
    }

    public void addLightAttackDamage(float value)
    {
        lightAttackDamage += value;
    }

    public void addMediumAttackDamage(float value)
    {
        mediumAttackDamage += value;
    }

    public void addHeavyAttackDamage(float value)
    {
        heavyAttackDamage += value;
    }

    public static void addKills(int value)
    {
        kills += value;
    }

    public static void addDeath(int value)
    {
        death += value;
    }




    private void recalculateStats() // recalculates current stats based on equipment
    {
        foreach (Equipment e in EquipmentManager.instance.currentEquipment)
        {
            float lDamage = 0f;
            float mDamage = 0f;
            float hDamage = 0f;
            float hpRegen = 0f;
            float itemDefence = 0f;

            if (e != null)
            {
                lDamage += e.lightAttackModifier;
                mDamage += e.mediumAttackModifier;
                hDamage += e.heavyAttackModifier;
                hpRegen += e.hpRegenModifier;
                itemDefence += e.armorModifier;
            }
            
            lightAttackDamage += lDamage;
            mediumAttackDamage += mDamage;
            heavyAttackDamage += hDamage;
            hpRegeneration += hpRegen;
            defence += itemDefence;

        }
    }


    public static void addTakenDamage(float value)
    {
        takenDamage += value;
    }

    public static void addGivenDamage(float value)
    {
        givenDamage += value;
    }


    private void CheckDataForLoad()
    {
        if (File.Exists(LSMain.knightDataPath))
        {

        }
        else
        {
            level = thisLevel;
            kills = thisKills;
            death = thisDeath;
            experience = thisExperience;
            takenDamage = thisTakenDamage;
            givenDamage = thisGivenDamage;
        }
    }

}
