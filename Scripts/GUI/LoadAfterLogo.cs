using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAfterLogo : MonoBehaviour
{
    public GameObject scenesLoader;

    public void LoadMainMenu()
    {
        scenesLoader.GetComponent<ScenesLoader>().LoadLevelByName("Menu_general");
    }
}
