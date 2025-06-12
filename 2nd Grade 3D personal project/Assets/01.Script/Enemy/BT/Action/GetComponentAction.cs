using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetComponent", story: "get components from [Self]", category: "Action", id: "80ec47f97162d1ca1b787a04116a12d9")]
public partial class GetComponentAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    protected override Status OnStart()
    {
        Enemy enemy = Self.Value;

        List<BlackboardVariable> varList = enemy.BtAgent.BlackboardReference.Blackboard.Variables;

        foreach (BlackboardVariable variable in varList)
        {
            if (typeof(IEntityComponent).IsAssignableFrom(variable.Type) == false) continue;

            SetComponent(enemy, variable.Name, enemy.GetCompo(variable.Type));
        }
        //SetVariable(enemy, "MainAnimator", enemy.GetCompo<EntityAnimator>());
        //SetVariable(enemy, "NavMovement", enemy.GetCompo<NavMovement>());
        return Status.Success;
    }

    private void SetComponent(Enemy enemy, string varName, IEntityComponent component)
    {
        if (enemy.BtAgent.GetVariable(varName, out BlackboardVariable variable))
        {
            variable.ObjectValue = component;
        }
    }

    private void SetVariable<T>(Enemy enemy, string varName, T component)
    {
        Debug.Assert(component != null, $"Check {varName} in {enemy.name}");
        BlackboardVariable<T> target = enemy.GetBlackboardVariable<T>(varName);
        target.Value = component;
    }
}

