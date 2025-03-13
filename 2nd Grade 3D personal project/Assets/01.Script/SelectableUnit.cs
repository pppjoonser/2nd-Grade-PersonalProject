using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;
    private void Awake()
    {
        SelectionManager.Instance.AvilableUnits.Add(this);
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
    }

    public void OnDeselectied()
    {
        SelectionSprite.gameObject.SetActive(false);
    }
}
