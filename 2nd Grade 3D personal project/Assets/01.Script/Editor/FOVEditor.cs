using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntityChecker))]
public class FOVEditor : Editor
{
    void OnSceneGUI()
    {
        EntityChecker checker = (EntityChecker)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(checker.transform.position, Vector3.up, Vector3.forward, 360, checker.SerchingRadius);
        Vector3 viewAngleL = checker.DirFromAngle(-checker.SerchingAngle / 2);
        Vector3 viewAngleR = checker.DirFromAngle(checker.SerchingAngle / 2);

        Handles.DrawLine(checker.transform.position, checker.transform.position + viewAngleL * checker.SerchingRadius);
        Handles.DrawLine(checker.transform.position, checker.transform.position + viewAngleR * checker.SerchingRadius);
    }
}
