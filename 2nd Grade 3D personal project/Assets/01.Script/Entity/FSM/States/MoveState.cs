using UnityEngine;
using UnityEngine.AI;

public class MoveState : EntityState
{
    private NavMovement agent;
    public MoveState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        agent = entity.GetComponent<NavMovement>();
    }

    public override void Update()
    {
        base.Update();
        if(agent.IsArrived)
        {
            _entity.ChageState("IDLE");
        }
    }
}
