using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragHandleUI : MonoBehaviour
{
    public static DragHandleUI Instance { get; private set; }
    private Image dragingImage;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public ItemSO draggingItem { get; private set; }
    public ItemSlot beforeSlot { get; private set; }

    private void Awake()
    {
        Instance = this;
        dragingImage = GetComponent<Image>();
        rectTransform = transform as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
        gameObject.SetActive(false);
    }
    public void SetDrag(ItemSO item, Vector3 position, ItemSlot slot)
    {
        gameObject.SetActive(true);
        draggingItem = item;
        dragingImage.sprite = item.itemImage;
        rectTransform.position = position;
        canvasGroup.blocksRaycasts = false;
        beforeSlot = slot;
    }
    public void EndDrag()
    {
        draggingItem = null;
        gameObject.SetActive(false);
        canvasGroup.blocksRaycasts = true;
    }

    public void MoveDrag(Vector3 position)
    {
        rectTransform.position = position;
    }
    public void SwapSlot(ItemSlot newSlot)
    {
        if (beforeSlot.currentItem == null)
            return;
        ItemSO temp = newSlot.currentItem;
        newSlot.SetItem(beforeSlot.currentItem);
        beforeSlot.SetItem(temp);
    }
}
