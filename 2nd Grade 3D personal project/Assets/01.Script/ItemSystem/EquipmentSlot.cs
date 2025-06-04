using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : ItemSlot
{
    [SerializeField] protected EquipmentType ableType;
    public override void OnDrop(PointerEventData eventData)
    {
        if(DragHandleUI.Instance.draggingItem.type == ableType)
        {
            DragHandleUI.Instance.SwapSlot(this);
        }
    }
}
