using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindTarget", story: "Find [Target] With [Checker]", category: "Action", id: "0eb81d9a1b4b92cdaf2649e295f4cd59")]
public partial class FindTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityChecker> Checker;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        FindTarget();
        return Status.Success;
    }

    private void FindTarget()
    {
        Transform findTarget = Checker.Value.FindCloseTarget();
        Target.Value = findTarget;
    }
}

