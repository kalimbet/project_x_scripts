using UnityEngine;

// Base class tp derive equipment from

[CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public float hpRegenModifier;
    public float armorModifier;
    public float lightAttackModifier;
    public float mediumAttackModifier;
    public float heavyAttackModifier;
    public Rarity rarity;   

    public override void Use()
    {
        base.Use();
        if(EquipmentManager.instance.Equip(this))
            RemoveFromInventory();
    }
}

public enum Rarity { Common, Uncommon, Rare, Legendary }
