using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsInAttack", story: "[Target] is in [Self] attack range", category: "Conditions", id: "258c91c76559428524e26735b14a8e58")]
public partial class IsInAttackCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    public override bool IsTrue()
    {
        float distance = Vector3.Distance(Target.Value.position, Self.Value.transform.position);
        return distance < Self.Value.attackRange;
    }
}
