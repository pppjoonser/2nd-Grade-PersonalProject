using System.Collections.Generic;
using UnityEngine;

public class EquipmentUIPanel : MonoBehaviour
{
    public List<EquipmentSlot> slots = new List<EquipmentSlot>();
    public void SetUnitItem(ItemSO[] items)
    {
        for(int i = 0; i < 10; i++)
        {
            slots[i].SetItem(items[i]);
        }
    }
}
