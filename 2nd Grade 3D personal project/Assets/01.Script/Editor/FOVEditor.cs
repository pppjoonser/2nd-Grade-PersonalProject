using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(EntityChecker))]
public class FOVEditor : Editor
{
    void OnSceneGUI()
    {
        EntityChecker checker = (EntityChecker)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(checker.transform.position, Vector3.up, Vector3.forward, 360, checker.SerchingRadius);
        Vector3 viewAngleL = checker.DirFromAngle(-checker.SerchingAngle / 2, false);
        Vector3 viewAngleR = checker.DirFromAngle(checker.SerchingAngle / 2, false);

        Handles.DrawLine(checker.transform.position, checker.transform.position + viewAngleL * checker.SerchingRadius);
        Handles.DrawLine(checker.transform.position, checker.transform.position + viewAngleR * checker.SerchingRadius);

        Handles.color = Color.cyan;
        foreach(Transform visibletarget in checker.visibleTargets)
        {
            Handles.DrawLine(checker.transform.position, visibletarget.position);
        }
    }
}
#endif