using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGUI : MonoBehaviour
{
    private TextMeshProUGUI knightLevel;


    void Start()
    {
        knightLevel = GetComponent<TextMeshProUGUI>();    
    }

    void Update()
    {
        knightLevel.text = KnightMain.level.ToString();
    }
}
