using UnityEngine;
using UnityEngine.AI;

public class NavmeshMover : MonoBehaviour
{
    protected NavMeshAgent _navAgent;

    protected virtual void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void AgentMove(Vector3 targetPosition)
    {
        _navAgent.destination = targetPosition;
    }
}
