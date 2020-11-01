using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDatabase", menuName = "Inventory/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    #region Singleton
    private static ItemDatabase instance;
    public static ItemDatabase Instance { get { return instance; } }
    public ItemDatabase()
    {
        instance = this;
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        instance = Resources.LoadAll<ItemDatabase>("")[0]; // load from default Resources folder
    }
    #endregion

    public Dictionary<Item, int> GetID;
    public Dictionary<int, Item> GetItem;

    public Item[] Items; // Add all ingame items in the inspector field

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<Item, int>();
        GetItem = new Dictionary<int, Item>();

        for (int i = 0; i < Items.Length; i++)
        {
            if(Items[i] != null)
            {
                GetID.Add(Items[i], i);
                GetItem.Add(i, Items[i]);
            }
        }
    }

    public void OnBeforeSerialize()
    {
    }

}
