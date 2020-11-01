using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string btn_play = "Play";
    public string btn_inventory = "Inventory";
    public string btn_settings = "Settings";
    public string btn_exit = "Exit";
    public string btn_invBack = "InvBack";
    public GameObject scenesLoader;

    void Update()
    {
        if (SimpleInput.GetButtonDown(btn_play)) { scenesLoader.GetComponent<ScenesLoader>().LoadLevelByName("Level_1"); }
        if (SimpleInput.GetButtonDown(btn_inventory)) { Debug.Log("Inventory"); }
        if (SimpleInput.GetButtonDown(btn_settings)) { Debug.Log("Settings"); }
        if (SimpleInput.GetButtonDown(btn_exit)) { Application.Quit(); }
        if (SimpleInput.GetButtonDown(btn_invBack)) { Debug.Log("Saving Inventory State"); SInventoryData.saveInvDataToFile(); }
    }
}
