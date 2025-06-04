using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : Entity
{
    private NavMeshAgent _agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;
    private List<ItemSO> items = new List<ItemSO>();
    protected override void Awake()
    {
        base.Awake();
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
    protected virtual void Update()
    {
        if (_agent.isStopped)
        {
            
        }
    }
    public void OnInventoryOpen()
    {
        foreach (ItemSO item in items)
        {
            ItemManager.Instance.Add(item);
        }
    }
}
