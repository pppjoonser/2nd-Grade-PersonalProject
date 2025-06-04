using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> slots = new List<ItemSlot>();

    [SerializeField] private Transform slotContent;
    [SerializeField] private GameObject slotPrefab;

    private void OnEnable()
    {
        SetSlots(ItemManager.Instance.SlotCount);
        UpdateItem();
    }
    public void SetSlots(int count)
    {
        for(int i = slots.Count; i < count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotContent);
            slot.name = $"slot {i}";
            slots.Add( slot.GetComponent<ItemSlot>());
        }
    }
    public void UpdateItem()
    {
        int i;
        for(i = 0; i < ItemManager.Instance.items.Count; i++)
        {
            slots[i].SetItem(ItemManager.Instance.items[i]);
        }
        i++;
        for(; i < slots.Count; i++)
        {
            slots[i].SetItem(null);
        }
    }
}
