using UnityEngine;
using UnityEngine.AI;

public class MoveState : EntityState
{
    private NavMeshAgent agent;
    public MoveState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        agent = entity.GetComponent<NavMeshAgent>();
    }

    public override void Update()
    {
        base.Update();
        if(agent.remainingDistance < 0.2f)
        {
            _entity.ChageState("IDLE");
        }
    }
}
