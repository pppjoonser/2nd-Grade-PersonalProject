using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RotateByTrigger", story: "[Movement] rotate to [Target] by [Trigger]", category: "Action", id: "b5e630d0620ca06fb74e816071f1839a")]
public partial class RotateByTriggerAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMovement> Movement;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> Trigger;
    private bool _isRotate;
    protected override Status OnStart()
    {
        _isRotate = false;
        Trigger.Value.OnManualRotationTrigger += HandleManualRotation;
        return Status.Running;
    }

    private void HandleManualRotation(bool obj) => _isRotate = obj;

    protected override Status OnUpdate()
    {
        if (_isRotate)
            Movement.Value.LookAtTarget(Target.Value.position);
        return Status.Success;
    }

    protected override void OnEnd()
    {
        Trigger.Value.OnManualRotationTrigger -= HandleManualRotation;
    }
}

