using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightMotionLogic : MonoBehaviour
{
    public bool onGround;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform")) // optimization in comparison method
        {
            onGround = true;
            Debug.Log(onGround);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Platform")) // optimization in comparison method
        {
            onGround = false;
            Debug.Log(onGround);
        }
    }
}
