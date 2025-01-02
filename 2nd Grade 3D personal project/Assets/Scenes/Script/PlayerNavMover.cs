using UnityEngine;

public class PlayerNavMover : NavmeshMover
{
    [SerializeField]
    private InputReader _input;
    private void OnEnable()
    {
        _input.OnClick += GetRayTargetCamera;
    }
    protected override void AgentMove(Vector3 targetPosition)
    {
        base.AgentMove(targetPosition);
    }
    private void OnDisable()
    {
        _input.OnClick -= GetRayTargetCamera;
    }

    private void GetRayTargetCamera()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            AgentMove(hitInfo.point);
        }
    }
}
