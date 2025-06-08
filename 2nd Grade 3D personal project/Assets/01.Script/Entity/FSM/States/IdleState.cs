using UnityEngine;
using UnityEngine.AI;

public class IdleState : EntityState
{
    private NavMeshAgent agent;
    public IdleState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        
    }
}
