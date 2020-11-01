using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInfoInGameObj : MonoBehaviour
{
    public GameObject textInfo, textInfoPref;
    private TextInfoInGame[] textInfoPool = new TextInfoInGame[15];
    private int numberOfInfoJbj = 0;
    
    void Start()
    {
        for (int i = 0; i < textInfoPool.Length; i++)
        {
            textInfoPool[i] = Instantiate(textInfoPref, textInfo.transform).GetComponent<TextInfoInGame>();
        }
    }

    void Update()
    {
        
    }

    public void ShowDamage(float value, string type)
    {
        textInfoPool[numberOfInfoJbj].Damage(type);
        textInfoPool[numberOfInfoJbj].StartMotion(Mathf.Round(value).ToString());
        numberOfInfoJbj = numberOfInfoJbj == textInfoPool.Length - 1 ? 0 : numberOfInfoJbj + 1;
    }

    public void ShowTreatment(float value)
    {
        textInfoPool[numberOfInfoJbj].Treatment();
        textInfoPool[numberOfInfoJbj].StartMotion(Mathf.Round(value).ToString());
        numberOfInfoJbj = numberOfInfoJbj == textInfoPool.Length - 1 ? 0 : numberOfInfoJbj + 1;
    }

    public void ShowBlocked()
    {
        textInfoPool[numberOfInfoJbj].Blocked();
        textInfoPool[numberOfInfoJbj].StartMotion(TextsLogic.blockedText);
        numberOfInfoJbj = numberOfInfoJbj == textInfoPool.Length - 1 ? 0 : numberOfInfoJbj + 1;
    }
}
