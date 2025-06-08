using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RotateState : EntityState
{
    NavMeshAgent agent;
    public RotateState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        agent = entity.GetComponent<NavMeshAgent>();
    }

    public override void Update()
    {
        if (RotateTowardsTargetY())
        {
            _entity.ChageState("IDLE");
        }
    }
    bool RotateTowardsTargetY()
    {
        Vector3 direction = _entity.RotationDestination - _entity.transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < Mathf.Epsilon) return true;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        _entity.transform.rotation = Quaternion.RotateTowards(
            _entity.transform.rotation,
            targetRotation,
            agent.angularSpeed * Time.deltaTime
        );
        return false;
    }
}
