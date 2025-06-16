using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RealatedEquipmentSlot : EquipmentSlot
{
    [SerializeField] private EquipmentType doubleEquipmentType;
    [SerializeField] private RealatedEquipmentSlot realatedSlot;

    public override void OnDrop(PointerEventData eventData)
    {
        if (DragHandleUI.Instance.draggingItem.type == ableType)
        {
            DragHandleUI.Instance.SwapSlot(this);
        }
        else if (DragHandleUI.Instance.draggingItem.type == doubleEquipmentType)
        {
            DragHandleUI.Instance.SwapSlot(this);
        }
    }

    public override void SetItem(ItemSO item)
    {
        if (itemImage == null)
        {
            itemImage = GetComponent<Image>();
        }
        if (realatedSlot.itemImage == null)
        {
            realatedSlot.itemImage = realatedSlot.GetComponent<Image>();
        }

        if(currentItem != null && currentItem.type == doubleEquipmentType)
        {
            if(item == null)
            {
                realatedSlot.currentItem = null;
                realatedSlot.itemImage.sprite = null;
            }
            else if(item.type == doubleEquipmentType)
            {
                realatedSlot.currentItem = item;
                realatedSlot.itemImage.sprite = item.itemImage;
            }
            else
            {
                realatedSlot.currentItem = null;
                realatedSlot.itemImage.sprite = null;
            }
        }
        else if(item != null)
        {
            if(item.type == doubleEquipmentType)
            {
                realatedSlot.currentItem = item;
                realatedSlot.itemImage.sprite = item.itemImage;
            }
        }

        currentItem = item;
        itemImage.sprite = (item != null ? item.itemImage : null);
    }
}
