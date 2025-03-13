using System.Collections.Generic;
using UnityEngine;

public class EntityChecker : MonoBehaviour
{
    [SerializeField]
    private float _serchingRadius;
    [SerializeField, Range(0,360)]
    private float _serchingAngle;
    [SerializeField]
    private LayerMask _targetType;

    [SerializeField]
    private float _serchingFrequency;

    private List<Vector3> dirList = new List<Vector3>();
    private List<Collider> detectList = new List<Collider>();

    [SerializeField]
    private Transform _forward;
    protected void CheckingInRadious()
    {
        Collider[] enemyInRadious = Physics.OverlapSphere(transform.position, _serchingRadius, _targetType);
        Vector3 forward = (_forward.position - transform.position).normalized;
        foreach (Collider coll in enemyInRadious)
        {
            Vector3 tDir = (coll.transform.position - transform.position).normalized;
            float tDot = Vector3.Dot(tDir, forward);
            if (tDot <= Mathf.Cos(_serchingAngle * Mathf.Deg2Rad))
            {
                float distanceToTarget = Vector3.Distance(transform.position, coll.transform.position);

                if (Physics.Raycast(transform.position, tDir, out RaycastHit hit, Vector3.Distance(coll.transform.position, transform.position), _targetType))
                {
                    detectList.Add(coll);
                }
            }
        }
    }
    protected void Seeking()
    {
        detectList.Clear();

        foreach(Vector3 dir in dirList)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,dir, out hit, _serchingRadius, _targetType))
            {
                detectList.Add(hit.collider);
            }
        }
    }
}
