using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsInDetect", story: "[Target] is in [self] detectRange", category: "Conditions", id: "254c5bbfed6144128be2b38de5e0646c")]
public partial class IsInDetectCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    public override bool IsTrue()
    {
        float distance = Vector3.Distance(Target.Value.position, Self.Value.transform.position);
        return distance < Self.Value.attackRange;
    }
}
