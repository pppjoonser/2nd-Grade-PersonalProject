using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForTrigger", story: "Wait for [Trigger] end", category: "Action", id: "96611fd0a34bb3a6221039d49b33c51e")]
public partial class WaitForTriggerAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> Trigger;
    private bool _isTriggered;
    protected override Status OnStart()
    {
        _isTriggered = false;
        Trigger.Value.OnAnimationEndTrigger += HandleAnimationEnd;
        return Status.Running;
    }

    private void HandleAnimationEnd() => _isTriggered = true;

    protected override Status OnUpdate()
    {
        return _isTriggered ? Status.Success : Status.Running;
    }
    protected override void OnEnd()
    {
        Trigger.Value.OnAnimationEndTrigger -= HandleAnimationEnd;
    }
}

