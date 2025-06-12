using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTarget", story: "[Movement] move to [target]", category: "Action", id: "b6865844e0c496e82ee5503b544689e2")]
public partial class MoveToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMovement> Movement;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Movement.Value.SetDestination(Target.Value.position);
        return Status.Success;
    }
}

