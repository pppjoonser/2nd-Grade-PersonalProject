using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RotateState : EntityState
{
    private NavMovement _agent;

    private float _startTime;
    public RotateState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _agent = entity.GetCompo<NavMovement>();
    }
    public override void Enter()
    {
        base.Enter();
        _startTime = Time.time;
    }
    public override void Update()
    {
        LookTargetSmoothly();
        if (Time.time - _startTime >= 1)
        {
            _entity.ChageState("IDLE");
        }
    }

    private void LookTargetSmoothly()
    {
        Vector3 direction = _entity.RotationDestination;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        Quaternion rotation = Quaternion.Slerp(
            _entity.transform.rotation,
            targetRotation,
            Time.deltaTime * _agent.RotationSpeed);

        _entity.transform.rotation = rotation;
    }
}
