using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60; //magic 
        LKnightData.LoadKnightDataFromFile();
        LInventoryData.LoadInvDataFromFile(); // TODO: Change INV loading to a different scene.
    }
}
