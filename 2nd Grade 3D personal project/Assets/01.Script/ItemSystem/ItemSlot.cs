using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemSO currentItem{get; protected set;} = null;
    public Image itemImage { get; protected set;}

    
    public virtual void SetItem(ItemSO item)
    {
        if(itemImage == null)
        {
            itemImage = GetComponent<Image>();
        }
        currentItem = item;
        itemImage.sprite = (item != null ? item.itemImage : null);
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        DragHandleUI.Instance.SwapSlot(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            DragHandleUI.Instance.SetDrag(currentItem, eventData.position, this);
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        DragHandleUI.Instance.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem != null)
            DragHandleUI.Instance.MoveDrag(eventData.position);
    }
}
