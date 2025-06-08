using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class EquipmentUIPanel : MonoBehaviour
{
    public List<EquipmentSlot> slots = new List<EquipmentSlot>();
    [SerializeField]
    private EventDataSO InvenEvent;

    private void Awake()
    {
        InvenEvent.OpenSlot += SetUnitItem;
    }

    public void SetUnitItem(ItemSO[] items)
    {
        for(int i = 0; i < 10; i++)
        {
            slots[i].SetItem(items[i]);
        }
    }

    public void SendUnitItem()
    {
        ItemSO[] settingItem = new ItemSO[10];
        for (int i = 0; i < 10; i++)
        {
            settingItem[i] = slots[i].currentItem;
        }
        InvenEvent.UnitItemSetting?.Invoke(settingItem);
    }
}
