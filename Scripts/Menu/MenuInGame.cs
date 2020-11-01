using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    public string btn_menu = "Menu";
    public string btn_resume = "Resume";
    public string btn_back = "Back";
    public GameObject scenesLoader;

    void Update()
    {
        if (SimpleInput.GetButtonDown(btn_menu)) Time.timeScale = 0;
        if (SimpleInput.GetButtonDown(btn_resume)) Time.timeScale = 1;
        if (SimpleInput.GetButtonDown(btn_back))
        {
            Time.timeScale = 1;
            scenesLoader.GetComponent<ScenesLoader>().LoadLevelByName("Menu_general");
        }
    }
}
