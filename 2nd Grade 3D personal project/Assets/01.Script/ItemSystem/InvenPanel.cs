using UnityEngine;

public class InvenPanel : MonoBehaviour
{
    [SerializeField] EventDataSO invenOpenEvent;

    private void Awake()
    {
        invenOpenEvent.OpenSlot += (a) => { gameObject.SetActive(true); };
        gameObject.SetActive(false);
    }
}
