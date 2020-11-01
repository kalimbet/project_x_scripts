using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightLevelSystem : MonoBehaviour
{
    public BarLogic expBar;
    private float expCurentUpgrade = 100;
    private float expNextUpgrade = 1;
    private float experienceMultiplier = 3.3f;
    private float diference;
    

    void Start()
    {
        DetectCurentUpgrade();        
    }

    void Update()
    {
        expBar.SetValue(KnightMain.experience);
        LevelUpgrade();
    }

    private void LevelUpgrade()
    {
        if ( KnightMain.experience > expNextUpgrade)
        {
            KnightMain.addLevel(1);
            diference = KnightMain.experience - expNextUpgrade;
            expBar.SetMinValue(KnightMain.experience - diference);
            NextUpgrade();  
        } 
    }

    private void DetectCurentUpgrade()
    {
        expNextUpgrade = expCurentUpgrade;
        for (int i = 0; i < KnightMain.level; i++)
        {
            expNextUpgrade *= experienceMultiplier;
            Debug.Log(expNextUpgrade);
        }
        expBar.SetMaxValue(expNextUpgrade);
    }
    
    private void NextUpgrade()
    {
        expNextUpgrade *= experienceMultiplier;
        //KnightMain.experience = diference;
        expBar.SetMaxValue(expNextUpgrade);
        Debug.Log("Next lvl" + expNextUpgrade);
    }
}
