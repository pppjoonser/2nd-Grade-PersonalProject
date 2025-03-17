using System.Collections.Generic;
using UnityEngine;

public class EntityChecker : MonoBehaviour
{
    [SerializeField]
    private float _serchingRadius;
    public float SerchingRadius => _serchingRadius;

    [SerializeField, Range(0,360)]
    private float _serchingAngle;
    public float SerchingAngle => _serchingAngle;
    [SerializeField]
    private LayerMask _targetType;

    [SerializeField]
    private float _serchingFrequency;

    private List<Vector3> dirList = new List<Vector3>();
    private List<Collider> detectList = new List<Collider>();

    [SerializeField]
    private Transform _forward;
    
    public  Vector3 DirFromAngle(float angleInDegrees, bool _isGrobalRange)
    {
        if (_isGrobalRange==false)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
