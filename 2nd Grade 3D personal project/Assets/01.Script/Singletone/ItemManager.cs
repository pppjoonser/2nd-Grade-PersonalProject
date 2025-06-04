using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public List<ItemSO> items;
    public int SlotCount = 40;
    private static ItemManager _instance;
    public static ItemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ItemManager();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    private  ItemManager()
    {
        items = new List<ItemSO>();
    }

    public void AddSlot(int amount)
    {
        SlotCount += amount;
    }

    public bool Add(ItemSO item)
    {
        if(items.Count < SlotCount)
        {
            items.Add(item);
            return true;
        }
        return false;
    }
    public void Remove(ItemSO item)
    {
        Debug.Assert(items.Count != 0, "there is no more Item!");
        items.Remove(item);
    }
}
