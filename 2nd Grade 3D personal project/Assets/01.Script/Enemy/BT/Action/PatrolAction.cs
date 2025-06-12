using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "[Self] patrol with [Points]", category: "Action", id: "824246fe16eb8bd95e4d58bfd5caf499")]
public partial class PatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;
    [SerializeReference] public BlackboardVariable<WayPoints> Points;

    private int _currentPointIdx;
    private NavMovement _navMovement;

    protected override Status OnStart()
    {
        Initialize();
        _navMovement.SetDestination(Points.Value[_currentPointIdx]);
        return Status.Running;
    }

    private void Initialize()
    {
        if (_navMovement == null)
            _navMovement = Self.Value.GetComponent<NavMovement>();
    }

    protected override Status OnUpdate()
    {
        if (_navMovement.IsArrived)
            return Status.Success;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        _currentPointIdx = (_currentPointIdx + 1) % Points.Value.Length;
    }
}

