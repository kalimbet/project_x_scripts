using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInfoInGame : MonoBehaviour
{
    private bool move;
    private Vector3 dir;
    public float speed = 0.5f;
    private Animation anim;
    private TextMeshProUGUI tx;


    private void Update()
    {
        if (!move) return;
        transform.Translate(dir * speed * Time.deltaTime);
        
    }

    private void Start()
    {
        anim = GetComponent<Animation>();
        tx = GetComponent<TextMeshProUGUI>();
    }

    public void StartMotion(string textInfo)
    {
        transform.localPosition = Vector3.zero;
        tx.text = textInfo;
        dir = new Vector2(Random.Range(-0.5f, 0.5f), 1);
        move = true;
        anim.Play();
    }

    public void StopMotion()
    {
        move = false;
    }

    public void Treatment()
    {
        tx.color = new Color(51/255f, 255/255f, 102/255f);
    }

    public void Damage(string type)
    {
        if(type == "Knight")
        {
            tx.color = Color.white;
        }
        else
        {
            tx.color = new Color(204 / 255f, 51 / 255f, 51 / 255f);
        }
        
    }

    public void Blocked()
    {
        tx.color = Color.white;
    }
}
