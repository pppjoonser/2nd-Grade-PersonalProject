using UnityEngine;

[DefaultExecutionOrder(-50)]
public class ItemGeter : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    private void Awake()
    {
        ItemManager.Instance.Add(item);
    }
}
