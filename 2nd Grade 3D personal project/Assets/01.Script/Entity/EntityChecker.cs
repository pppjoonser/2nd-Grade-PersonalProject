using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChecker : MonoBehaviour, IEntityComponent
{
    private Entity _owner;

    [SerializeField]
    private float _serchingRadius;
    [SerializeField] private float searchingHeight= 1.0f;
    public float SerchingRadius => _serchingRadius;

    [SerializeField, Range(0,360)]
    private float _serchingAngle;
    public float SerchingAngle => _serchingAngle;
    [SerializeField] private int edgeResolveIterations;
    [SerializeField] private float edgeDistanceThreshold;

    [SerializeField] private MeshFilter viewMeshFilter;
    Mesh viewMesh;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    [SerializeField] private float _serchingFrequency;

    [HideInInspector] public List<Transform> visibleTargets = new List<Transform>();

    [SerializeField, Range(0,1)] 
    private float meshResolution;
    private void Awake()
    {
        viewMesh = new Mesh();
        viewMesh.name = "FOV Mesh";
        viewMeshFilter.mesh = viewMesh;
    }
    private void Start()
    {
        
        StartCoroutine(FindTaregtsWithDelay(_serchingFrequency));
    }
    private IEnumerator FindTaregtsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SeekingInRange();
        }
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }
    private void SeekingInRange()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadious = Physics.OverlapSphere(transform.position, _serchingRadius, _targetMask);

        foreach(Collider target in targetsInViewRadious)
        {
            Transform calculatingTarget = target.transform;
            Vector3 dirToTarget = (calculatingTarget.position -transform.position).normalized;
            float searchingAngle = Mathf.Cos((SerchingAngle / 2) * Mathf.Deg2Rad);
            bool isInAngle = Vector3.Dot(transform.forward, dirToTarget) > searchingAngle;
            if(isInAngle)
            {
                float destinationToTarget = Vector3.Distance(transform.position, calculatingTarget.position);
                if(!Physics.Raycast (transform.position, dirToTarget, destinationToTarget, _obstacleMask))
                {
                    visibleTargets.Add(calculatingTarget);
                }
            }
        }
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt( _serchingAngle * meshResolution);
        float stepAngelSize = _serchingAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for(int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - _serchingAngle/2 + stepAngelSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if(i > 0)
            {
                bool edgeDistanceThresholdExceeded = 
                    Mathf.Abs(oldViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;
                if(oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDistanceThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if(edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i  = 0; i < edgeResolveIterations; i++)
        {
            float angle = minAngle + (maxAngle - minAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDistanceThresholdExceeded =
                    Mathf.Abs(minViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;

            if (minViewCast.hit == newViewCast.hit && !edgeDistanceThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

         return new EdgeInfo(minPoint, maxPoint);
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, dir, out hit, _serchingRadius, _obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * _serchingRadius, _serchingRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool _isGrobalRange)
    {
        if (_isGrobalRange == false)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
        {
            this.hit = hit;
            this.point = point;
            this.distance = dst;
            this.angle = angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            this.pointA = _pointA;
            this.pointB = _pointB;
        }
    }

    public void Initialize(Entity _entity)
    {
        _owner = _entity;
    }
}
