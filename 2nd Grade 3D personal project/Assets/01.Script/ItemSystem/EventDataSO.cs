using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventDataSO", menuName = "Scriptable Objects/EventDataSO")]
public class EventDataSO : ScriptableObject
{
    public Action<ItemSO[]> OpenSlot;
    public Action<ItemSO[]> UnitItemSetting;
}
