using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour, IEntityComponent
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float stopOffset = 0.05f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField]
    private float moveSpeed = 4f;
    private EntityStat _statCompo;
    public float RotationSpeed { 
        get
        {
            return rotationSpeed;
        }
        private set
        {
            rotationSpeed = value;
        }
    }
    private Entity _entity;

    public bool IsArrived => !agent.pathPending
                             && agent.remainingDistance <= agent.stoppingDistance + stopOffset;

    public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        agent.speed = moveSpeed;
        _statCompo = entity.GetCompo<EntityStat>();
    }

    private void Update()
    {
        if (agent.hasPath && agent.isStopped == false && agent.path.corners.Length > 0)
        {
            LookAtTarget(agent.steeringTarget, true);
        }
    }
    private void HandleMoveSpeedChange(StatSO stat, float currentValue, float prevValue)
    {
        moveSpeed = currentValue;
    }

    /// <summary>
    /// 지정한 Target위치로 회전하는 함수
    /// </summary>
    /// <param name="target">Vector3 - 바라볼 위치</param>
    /// <param name="isSmooth">boolean - Lerp 적용여부</param>
    public void LookAtTarget(Vector3 target, bool isSmooth = true)
    {
        Vector3 direction = target - _entity.transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

        if (isSmooth)
        {
            _entity.transform.rotation = Quaternion.Slerp(_entity.transform.rotation, lookRotation,
                Time.deltaTime * rotationSpeed);
        }
        else
        {
            _entity.transform.rotation = lookRotation;
        }
    }

    private void OnDestroy()
    {
        _entity.transform.DOKill();
    }

    public void SetStop(bool isStop) => agent.isStopped = isStop;
    public void SetVelocity(Vector3 velocity) => agent.velocity = velocity;
    public void SetSpeed(float speed) => agent.speed = speed;
    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);


    public void KnockBack(Vector3 force, float time)
    {
        SetStop(true);
        Vector3 destination = GetKnockBackEndPosition(force);
        Vector3 delta = destination - _entity.transform.position;
        float knockBackDuration = delta.magnitude * time / force.magnitude;

        _entity.transform.DOMove(destination, knockBackDuration)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
            {
                agent.Warp(transform.position);
                SetStop(false);
            });
    }

    private Vector3 GetKnockBackEndPosition(Vector3 force)
    {
        Vector3 startPosition = _entity.transform.position + new Vector3(0, 0.5f);
        if (Physics.Raycast(startPosition, force.normalized, out RaycastHit hit, force.magnitude))
        {
            Vector3 hitpoint = hit.point;
            hitpoint.y = _entity.transform.position.y;
            return hitpoint;
        }
        return _entity.transform.position + force;
    }
}
