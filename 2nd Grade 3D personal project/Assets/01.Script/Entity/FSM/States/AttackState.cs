using UnityEngine;

public class AttackState : EntityState
{
    private EntityAttackCompo _attackCompo;
    private NavMovement _movement;
    public AttackState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _attackCompo = entity.GetCompo<EntityAttackCompo>();
        _movement = entity.GetCompo<NavMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        _movement.SetStop(true);
        _attackCompo.Attack();
        ApplyAttackData();
    }
    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
            _entity.ChageState("CHASE");
    }

    private void ApplyAttackData()
    {
        AttackDataSO currentAttackData = _attackCompo.GetCurrentAttackData();
        Vector3 playerDirection = _entity.transform.forward;
        _entity.transform.rotation = Quaternion.LookRotation(playerDirection);
        Vector3 movement = playerDirection * currentAttackData.movementPower;
    }
    public override void Exit()
    {
        _attackCompo.EndAttack();
        _movement.SetStop(false);
        base.Exit();
    }
}
