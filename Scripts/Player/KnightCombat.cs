using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    //public float attackRate = 1f;
    //float nextAttackTime = 0f;
    bool attackCheck = false;
    bool attackSecondStatus = false;
    Animator heroAnimator;
    bool wasDown = false;



    void Start()
    {
        heroAnimator = GetComponent<Animator>();
    }

    void Update()
    {

        if (SimpleInput.GetButtonDown("Attack") && KnightMain.dodgeStatus == false && KnightMain.attackStatus == false)
        {
            heroAnimator.SetTrigger("Attack_1");
        }


            /*if (Time.time >= nextAttackTime)
            {
                if (SimpleInput.GetButtonDown("Attack") && KnightMain.dodgeStatus == false && KnightMain.attackStatus == false)
                {
                    //Set attackStatus and play attack animation
                    heroAnimator.SetTrigger("Attack_1");
                    //Start Timer
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }*/


            if (attackCheck == true)
        {
            if (SimpleInput.GetButtonDown("Attack") && KnightMain.dodgeStatus == false)
            {
                if(attackSecondStatus == false)
                {
                    heroAnimator.SetTrigger("Attack_2");
                    attackSecondStatus = true;
                }
                else
                {
                    heroAnimator.SetTrigger("Attack_3");
                }
            }
        }

        Block();
    }

    private void Block()
    {
        if (SimpleInput.GetButtonDown("Block"))
        {
            KnightMain.movementStatus = false;
            KnightMain.blockStatus = true;
            heroAnimator.SetInteger("State", 3);
            wasDown = true;
        }

        if (SimpleInput.GetButtonUp("Block") == true && wasDown == true)
        {
            KnightMain.movementStatus = true;
            KnightMain.blockStatus = false;
            heroAnimator.SetInteger("State", 4);
            wasDown = false;
        }
    }


    public void LightAttack()
    {
        //Damage enemies
        HitEnemy(KnightMain.lightAttackDamage);
    }

    public void MediumAttack()
    {
        //Damage enemies
        HitEnemy(KnightMain.mediumAttackDamage);
    }

    public void HeavyAttack()
    {
        HitEnemy(KnightMain.heavyAttackDamage);
    }

    private void HitEnemy(float damage)
    {
        //Detect enemies in range of attack
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log("Applied Enemy Damage: " + damage);
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    public void SetAttackCheckTrue()
    {
        attackCheck = true;
    }

    public void SetAttackCheckFalse()
    {
        attackCheck = false;
    }

    public void SetAttackStatusTrue()
    {
        KnightMain.attackStatus = true;
    }

    public void SetAttackStatusFalse()
    {
        KnightMain.attackStatus = false;
    }

    public void SetAttackSecondStatusFalse()
    {
        attackSecondStatus = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
