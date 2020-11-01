using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RarityPartitioner", menuName = "Inventory/RarityPartitioner")]
public class ItemRarityPartitioner : ScriptableObject
{
    // prototype V2
    // runtime item distribution
    
    //public bool runtimeItemDetection = false;

    public List<Item> commonItems = new List<Item>();
    public List<Item> uncommonItems = new List<Item>();
    public List<Item> rareItems = new List<Item>();

    [NonSerialized]
    public List<List<Item>> fullDB = new List<List<Item>>();

    [NonSerialized]
    private ItemDatabase database;
    
    [NonSerialized]
    private bool initialized = false;

    public void FillArraysFromItemDB() // to be used for realtime allocation
    {
        database = ItemDatabase.Instance;

        if (!initialized) {
            commonItems = new List<Item>();
            uncommonItems = new List<Item>();
            rareItems = new List<Item>();

            foreach (Item i in database.Items)
            {
                Equipment equip = i as Equipment;
                if (equip != null)
                {
                    if (string.Equals(equip.rarity.ToString(), "Common"))
                        commonItems.Add(equip);

                    if (string.Equals(equip.rarity.ToString(), "Uncommon"))
                        uncommonItems.Add(equip);

                    if (string.Equals(equip.rarity.ToString(), "Rare"))
                        rareItems.Add(equip);
                }
            }

            fullDB.Add(commonItems);
            fullDB.Add(uncommonItems);
            fullDB.Add(rareItems);

            initialized = true;
        }
    }
}
