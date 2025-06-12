using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : Entity
{
    [SerializeField] private EventDataSO invenOpenEvent;
    private NavMovement _agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;
    [SerializeField]
    private ItemSO[] items;

    [SerializeField] private StateDataSO[] states;

    public Transform CurrentTarget { get; private set; }

    private EntityStateMachine stateMachine;

    public Vector3 RotationDestination { get; private set; }
    public bool IsMove { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        SelectionManager.Instance.AvilableUnits.Add(this);
        _agent = GetCompo<NavMovement>();
        stateMachine = new EntityStateMachine(this, states);
        invenOpenEvent.UnitItemSetting += SetItem;
    }

    private void SetItem(ItemSO[] items)
    {
        this.items = items;
    }

    public void AttackTarget(Transform target)
    {
        CurrentTarget = target;
        stateMachine.ChangeState("CHASE");
    }

    public void MoveTo(Vector3 position)
    {
        _agent.SetStop(false);
        _agent.SetDestination(position);
        stateMachine.ChangeState("MOVE");
    }

    public void RotateTo(Vector3 position)
    {
        RotationDestination = position-transform.position;
        _agent.SetStop(true);
        stateMachine.ChangeState("ROTATE");
    }

    protected override void Start()
    {
        stateMachine.ChangeState("IDLE");
    }

    private void Update()
    {
        stateMachine.UpdateStateMachine();
    }

    public void ChageState(string statename, bool forced = false) => stateMachine.ChangeState(statename, forced);

    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
    }

    public void OnDeselectied()
    {
        SelectionSprite.gameObject.SetActive(false);
    }

    public void OnInventoryOpen()
    {
        invenOpenEvent.OpenSlot.Invoke(items);
    }
}
