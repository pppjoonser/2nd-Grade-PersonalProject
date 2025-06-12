using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsHasValue", story: "[Target] Has Value", category: "Conditions", id: "5b2ccebc94dd2983e9ef52d5c9c428a5")]
public partial class IsHasValueCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;

    public override bool IsTrue()
    {
        if (Target.Value == null)
        {
            return false;   
        }
        return true;
    }
}
