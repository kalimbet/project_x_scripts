using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMotion : MonoBehaviour
{
    public BarLogic staminaBar;
    public float speed = 1f;
    public float forceJump = 3f;
    public float dodgeRate = 1f;
    public float dodgeForce = 7;
    public float dodgeForAttack = 5;
    public float dodgeForAttackForce = 2;
    public float staminaRegeneration = 10;
    public float staminaCost = 40;
    float nextDodgeTime = 0f;
    float moveDirection = 1;
    bool onGround = false;
    Rigidbody2D heroRb;
    Animator heroAnimator;
    float playerMotionX;
    float speedDef;
    float deadArea = 0.5f;
    float stamina = 100f;
    float staminaCurrent;
    

    // Start is called before the first frame update
    void Start()
    {
        heroRb = GetComponent<Rigidbody2D>();
        heroAnimator = GetComponent<Animator>();
        speedDef = speed;
        staminaCurrent = stamina;
        staminaBar.SetMaxValue(stamina);
    }

    private void Update()
    {
        Motion();
        StaminaLogic();
        Dodge();
        Jump();
    }

    void Motion()
    {
        playerMotionX = SimpleInput.GetAxis("Horizontal");

        if (KnightMain.movementStatus == true && KnightMain.dodgeStatus == false)
        {
            if(playerMotionX > 0)
            {
                
                if(KnightMain.attackStatus == true)
                {
                    speed = 0;
                    moveDirection = 1;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    if(playerMotionX <= deadArea)
                    {
                        heroRb.velocity = new Vector2(deadArea * speed, heroRb.velocity.y);
                    }
                    else
                    {
                        heroRb.velocity = new Vector2(playerMotionX * speed, heroRb.velocity.y);
                    }
                    
                }
                else
                {
                    speed = speedDef;
                    heroAnimator.SetInteger("State", 2);
                    moveDirection = 1;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    if (playerMotionX <= deadArea)
                    {
                        heroRb.velocity = new Vector2(deadArea * speed, heroRb.velocity.y);
                    }
                    else
                    {
                        heroRb.velocity = new Vector2(playerMotionX * speed, heroRb.velocity.y);
                    }
                }
                
            }else if (playerMotionX < 0)
            {
                if (KnightMain.attackStatus == true)
                {
                    speed = 0;
                    moveDirection = -1;
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    if (playerMotionX >= -deadArea)
                    {
                        heroRb.velocity = new Vector2(-deadArea * speed, heroRb.velocity.y);
                    }
                    else
                    {
                        heroRb.velocity = new Vector2(playerMotionX * speed, heroRb.velocity.y);
                    }
                }
                else
                {
                    speed = speedDef;
                    heroAnimator.SetInteger("State", 2);
                    moveDirection = -1;
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    if(playerMotionX >= -deadArea)
                    {
                        heroRb.velocity = new Vector2(-deadArea * speed, heroRb.velocity.y);
                    }
                    else
                    {
                        heroRb.velocity = new Vector2(playerMotionX * speed, heroRb.velocity.y);
                    }
                }
                    
            }
            else
            {
                heroAnimator.SetInteger("State", 1);
            }
        }
    }

    void StaminaLogic()
    {
        if(staminaCurrent < stamina)
        {
            staminaCurrent += staminaRegeneration * Time.deltaTime;
            staminaBar.SetValue(staminaCurrent);
        }
    }
    void Jump()
    {
        if (KnightMain.movementStatus == true)
        {
            if (staminaCurrent - staminaCost > 0)
            {
                if (SimpleInput.GetButtonDown("Jump") == true && onGround == true)
                {
                    ResetPlayerPosition();
                    heroRb.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
                    staminaCurrent -= staminaCost;
                    onGround = false;
                }
            }
        }
    }

    void Dodge()
    {
        if (Time.time >= nextDodgeTime)
        {
            if(staminaCurrent - staminaCost > 0)
            {
                if (SimpleInput.GetButtonDown("Dodge") == true && moveDirection != 0 && KnightMain.attackStatus == false)
                {
                    KnightMain.dodgeStatus = true;
                    heroAnimator.SetTrigger("Dodge");
                    staminaCurrent -= staminaCost;
                    if (moveDirection > 0) { heroRb.AddForce(new Vector2(1, 0) * dodgeForce, ForceMode2D.Impulse); }
                    else { heroRb.AddForce(new Vector2(-1, 0) * dodgeForce, ForceMode2D.Impulse); }
                    nextDodgeTime = Time.time + dodgeRate;
                }
            }
            
        }
    }

    public void SetDodgeStatusFalse()
    {
        KnightMain.dodgeStatus = false;
    }

    public void DodgeForAttack()
    {
        if(moveDirection > 0) {
            heroRb.AddForce(new Vector2(1, 0) * dodgeForce, ForceMode2D.Impulse);
        }
        else
        {
            heroRb.AddForce(new Vector2(-1, 0) * dodgeForce, ForceMode2D.Impulse);
        }
    }

    public void ResetPlayerPosition()
    {
        heroRb.velocity = new Vector2(0, 0);
    }

    public void TakeDamageBlockMotion()
    {
        if (moveDirection > 0)
        {
            heroRb.AddForce(new Vector2(-1, 0) * dodgeForAttackForce, ForceMode2D.Impulse);
        }
        else
        {
            heroRb.AddForce(new Vector2(1, 0) * dodgeForAttackForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            onGround = true;
        }
        
    }
}

