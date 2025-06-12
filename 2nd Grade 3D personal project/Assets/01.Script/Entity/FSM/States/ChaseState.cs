using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EntityState
{
    private EntityChecker checker;
    private NavMovement agent;
    private float restartTime;
    public ChaseState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        checker = entity.GetCompo<EntityChecker>();
        agent = entity.GetCompo<NavMovement>();
    }
    public override void Enter()
    {
        base.Enter();
        restartTime = Time.time;
    }
    public override void Update()
    {
        base.Update();
        Attack();
    }

    private void Attack()
    {

        if (Vector3.Distance(_entity.CurrentTarget.position, _entity.transform.position) < 3)
        {
            _entity.ChageState("ATTACK");
        }
        agent.SetDestination(_entity.CurrentTarget.position);
    }
}
