using UnityEngine;
using TMPro;
public class KnightHealth : MonoBehaviour
{
    public BarLogic healthBar;
    private float currentHealth;
    private float currentDefence;
    private ScenesLoader deathScene;
    private TextInfoInGameObj tiigo;
    public TextMeshProUGUI healthGUI;
    public GameObject healthRegenObj;
    private TextMeshProUGUI healthRegenGUI;
    private Animation healthRegenGUIAnim;

    
    private float nextTime = 0;
    void Start()
    {
        currentHealth = KnightMain.health;
        currentDefence = KnightMain.defence;
        healthBar.SetMaxValue(KnightMain.health);
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
        deathScene = FindObjectOfType<ScenesLoader>();
        tiigo = GetComponentInChildren<TextInfoInGameObj>();
        healthRegenGUI = healthRegenObj.GetComponent<TextMeshProUGUI>();
        healthRegenGUIAnim = healthRegenObj.GetComponent<Animation>();
        HealthRegenerationPrepare();
    }

    private void Update()
    {
        HealthRegeneration();
    }

    public void TakeDamage(float damage)
    {
        if (!KnightMain.blockStatus)
        {
            float damageReductionPercentage = currentDefence * 100 / KnightMain.maxDefenceRating;
            damageReductionPercentage = Mathf.Clamp(damageReductionPercentage, 0, 99); // set min defence to 0%, max to 99%
            //Debug.LogWarning("REDUCED DAMAGE: " + damageReductionPercentage);

            damage *= (100 - damageReductionPercentage) / 100;
            damage = Mathf.Clamp(damage, 0, int.MaxValue);

            currentHealth -= damage;
            healthBar.SetValue(currentHealth);
            KnightMain.addTakenDamage(damage);
            tiigo.ShowDamage(damage,"Knight");
            //Debug.Log("Took damage: " + damage);
        }
        else
        {
            GetComponent<KnightMotion>().TakeDamageBlockMotion();
            tiigo.ShowBlocked();
            //Debug.Log("Blocked!");
        }

        //play animation
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void HealthRegeneration()
    {
        if (currentHealth < KnightMain.health && currentHealth > 0)
        {
            currentHealth += KnightMain.hpRegeneration * Time.deltaTime;
            healthBar.SetValue(currentHealth);
            HealthRegenerationGui();
        }
/*
        if (currentHealth < KnightMain.health && currentHealth > 0)
        {
            currentHealth += KnightMain.hpRegeneration * Time.deltaTime;
            healthBar.SetValue(currentHealth);
        }*/
    }

    private void HealthRegenerationPrepare()
    {
        healthGUI.text = Mathf.Round(currentHealth).ToString() + " / " + KnightMain.health;
    }
    private void HealthRegenerationGui()
    {
        healthGUI.text = Mathf.Round(currentHealth).ToString() + " / " + KnightMain.health;
        if (Time.time >= nextTime)
        {
            //tiigo.ShowTreatment(KnightMain.hpRegeneration);
            healthRegenGUI.text = " +" + KnightMain.hpRegeneration.ToString();
            healthRegenGUIAnim.Play();
            nextTime = Time.time + 0.4f;
        }
        
    }
    private void Die()
    {
        KnightMain.addDeath(1);
        SKnightData.SaveKnightDataToFile();
        SInventoryData.saveInvDataToFile();
        Debug.Log("Hero died!");
        
        // Die animation

        // Reset variables
        KnightMain.blockStatus = false;
        KnightMain.movementStatus = true;
        KnightMain.attackStatus = false;
        KnightMain.dodgeStatus = false;

        deathScene.LoadLevelByName("Level_1");
    }

    void onEquipmentChanged(Equipment newItem, Equipment oldItem) 
        // Recalculate stats | performs ON EQUIPMENT CHANGE
        // TODO: Move attack damage calculation to another file?
    {
        float defenceRating = currentDefence;
       
        Debug.Log("Recalculating stats!");

        if(newItem != null) { 
            defenceRating += newItem.armorModifier;
            KnightMain.hpRegeneration += newItem.hpRegenModifier;
            KnightMain.lightAttackDamage += newItem.lightAttackModifier;
            KnightMain.mediumAttackDamage += newItem.mediumAttackModifier;
            KnightMain.heavyAttackDamage += newItem.heavyAttackModifier;
            
        }

        if (oldItem != null)
        {
            defenceRating -= oldItem.armorModifier;
            KnightMain.hpRegeneration -= oldItem.hpRegenModifier;
            KnightMain.lightAttackDamage -= oldItem.lightAttackModifier;
            KnightMain.mediumAttackDamage -= oldItem.mediumAttackModifier;
            KnightMain.heavyAttackDamage -= oldItem.heavyAttackModifier;

        }

        currentDefence = defenceRating;

    }
}
