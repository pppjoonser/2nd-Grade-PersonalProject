using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSO")]
public class StatSO : ScriptableObject, ICloneable
{
    public delegate void ValueChageHandler(StatSO stat, float currentValue, float prevValue);
    public event ValueChageHandler OnValueChage;

    public string statName;
    public string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private string displayName;
    [SerializeField] private float baseValue, minValue, maxValue;

    private Dictionary<object, float> _modifyValueByKey = new();

    [field:SerializeField] public bool IsPercent { get; private set; }

    private float _modifiedValue = 0;
    public Sprite Icon => icon;

    public float MaxValue
    {
        get => maxValue;
        set => maxValue = value;
    }

    public float MinValue
    {
        get => minValue;
        set => minValue = value;
    }

    public float Value => Mathf.Clamp(baseValue, minValue, maxValue);
    public bool IsMax => Mathf.Approximately(baseValue, maxValue);

    public bool IsMin => Mathf.Approximately(baseValue, minValue);

    public float BaseValue
    {
        get => baseValue;
        set
        {
            float prevValue = Value;
            baseValue = Mathf.Clamp(value, minValue, maxValue);
            TryInvokeValueChangeEvent(Value, prevValue);
        }
    }

    public void AddModifier(object key, float value)
    {
        if (_modifyValueByKey.ContainsKey(key)) return;

        float prevValue = Value;
        _modifiedValue += value;
        _modifyValueByKey.Add(key, prevValue);
        TryInvokeValueChangeEvent(value, prevValue);
    }

    public void RemoveModifier(object key)
    {
        if (_modifyValueByKey.TryGetValue(key, out float value))
        {
            float prevValue = Value;
            _modifiedValue -= value;
            _modifyValueByKey.Remove(key);
            TryInvokeValueChangeEvent(Value, prevValue);
        }
    }
    public void ClearModifier()
    {
        float prevValue = Value;
        _modifyValueByKey.Clear();
        _modifiedValue = 0;
        TryInvokeValueChangeEvent(Value, prevValue);
    }
    private void TryInvokeValueChangeEvent(float value, float prevValue)
    {
        //이전값과 바뀐값이 일치하지 않느다면 변경 이벤트를 콜해주는 함수.
        if (Mathf.Approximately(value, prevValue))
        {
            OnValueChage?.Invoke(this, value, prevValue);
        }
    }

    public object Clone()
    {
        return Instantiate(this);
    }
}
