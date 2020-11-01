using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public BarLogic healthBar;
    private float currentHealth;
    private TextInfoInGameObj tiigo;
    private Animator anim;
    private bool alive;

    void Start()
    {
        alive = true;
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        tiigo = GetComponentInChildren<TextInfoInGameObj>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (alive)
        {
            currentHealth -= damage;
            KnightMain.addGivenDamage(damage);
            tiigo.ShowDamage(damage, "Enemy");
            healthBar.SetValue(currentHealth);
            //play animation
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        KnightMain.addKills(1);
        DeathRewards.ExperienceReward(gameObject);
        anim.SetTrigger("Death");
        alive = false;
        //Destroy(gameObject);
        // Die animation
    }

}
