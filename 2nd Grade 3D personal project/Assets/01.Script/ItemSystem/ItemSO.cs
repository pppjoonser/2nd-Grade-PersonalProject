using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemImage;
    public EquipmentType type;
}
