using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopMoveAction", story: "Set [Movement] isStop to [newValue]", category: "Action", id: "9c1f696a81ca0812892717f954b87988")]
public partial class StopMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMovement> Movement;
    [SerializeReference] public BlackboardVariable<bool> NewValue;

    protected override Status OnStart()
    {
        Movement.Value.SetStop(NewValue.Value);
        return Status.Success;
    }
}

