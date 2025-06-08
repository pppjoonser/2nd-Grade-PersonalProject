using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : Entity
{
    [SerializeField] private EventDataSO invenOpenEvent;
    private NavMeshAgent _agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;
    [SerializeField]
    private ItemSO[] items;

    [SerializeField] private StateDataSO[] states;
    private float tempSpeed;

    private EntityStateMachine stateMachine;

    public Vector3 RotationDestination { get; private set; }
    public bool IsMove { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        SelectionManager.Instance.AvilableUnits.Add(this);
        _agent = GetComponent<NavMeshAgent>();
        stateMachine = new EntityStateMachine(this, states);
        invenOpenEvent.UnitItemSetting += SetItem;
    }

    private void SetItem(ItemSO[] items)
    {
        this.items = items;
    }

    public void MoveTo(Vector3 position)
    {
        _agent.speed = tempSpeed;
        _agent.isStopped = false;
        _agent.SetDestination(position);
        stateMachine.ChangeState("MOVE");
    }

    public void RotateTo(Vector3 position)
    {
        RotationDestination = position;
        stateMachine.ChangeState("ROTATE");
    }

    protected override void Start()
    {
        stateMachine.ChangeState("IDLE");

        tempSpeed = _agent.speed;
    }

    private void Update()
    {
        stateMachine.UpdateStateMachine();
    }

    public void ChageState(string statename) => stateMachine.ChangeState(statename);

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
