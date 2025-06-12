using System;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private RectTransform _selectionBox;
    [SerializeField]
    private LayerMask unitLayers;
    [SerializeField]
    private LayerMask floorLayers;
    [SerializeField]
    private LayerMask enemyMask;
    [SerializeField]
    private InputReader _input;

    private bool draging = false;
    private Vector2 startMousePosition;
    private bool rotateOrderEnabled = false;

    private const float DRAG_FACTOR = 0.5f;
    private void Awake()
    {
        _input.OnClick += HandleDownInputs;
        _input.OnCancleClick += HandleCancleInputs;
        _input.OnMoveOrder += HandleMoveOrderInputs;
        _input.OnInventoryPressed += HandleInventoryOrderInputs;
        _input.OnRotateOrderPressed += HandleRotateOrderInputs;
    }

    private void HandleRotateOrderInputs()
    {
        rotateOrderEnabled = ! rotateOrderEnabled;
    }

    private void HandleInventoryOrderInputs()
    {
        if(SelectionManager.Instance.SelectedUnits.Count == 1)
        {
            SelectionManager.Instance.SelectedUnits.First().OnInventoryOpen();
        }
    }

    private void HandleMoveOrderInputs()
    {
        if(SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.value), out RaycastHit enemyhit, Mathf.Infinity, enemyMask))
            {
                foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    unit.AttackTarget(enemyhit.transform);
                }
            }
            else if (Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.value), out RaycastHit hit, Mathf.Infinity, floorLayers))
            {
                foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    if (rotateOrderEnabled == false)
                    {
                        unit.MoveTo(hit.point);
                    }
                    else
                    {
                        unit.RotateTo(hit.point);
                    }
                    rotateOrderEnabled = false;
                }
            }
           
        }
    }

    private void Update()
    {
        if (_input.HoldingSelect)
        {
            if (MathF.Abs(Mouse.current.delta.value.magnitude) > DRAG_FACTOR)
            {
                draging = true;
            }
        }

        if (draging)
        {
            DragFunction();
        }
    }
    private void HandleDownInputs()
    {
        _selectionBox.sizeDelta = Vector2.zero;
        _selectionBox.gameObject.SetActive(true);
        startMousePosition = Mouse.current.position.value;
    }
    private void HandleCancleInputs()
    {
        _selectionBox.sizeDelta = Vector2.zero;
        _selectionBox.gameObject.SetActive(false);

        if (Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.value), out RaycastHit hit, Mathf.Infinity, unitLayers)
            &&hit.collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
        {
            if(_input.HoldingMultiSelect)
            {
                if(SelectionManager.Instance.IsSelected(unit))
                {
                    SelectionManager.Instance.Deselect(unit);
                }
                else
                {
                    SelectionManager.Instance.Select(unit);
                }
            }
            else
            {
                SelectionManager.Instance.DeselectAll();
                SelectionManager.Instance.Select(unit);
            }
        }
        else if (draging == false)
        {
            SelectionManager.Instance.DeselectAll();
        }
        draging = false;
    }

    private void DragFunction()
    {
        ResizeSelectionBox();
    }


    private void ResizeSelectionBox()
    {
        float width = Mouse.current.position.value.x - startMousePosition.x;
        float height = Mouse.current.position.value.y - startMousePosition.y;

        _selectionBox.anchoredPosition = startMousePosition + new Vector2(width / 2, height / 2);
        _selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(_selectionBox.anchoredPosition, _selectionBox.sizeDelta);

        for (int i = 0; i < SelectionManager.Instance.AvilableUnits.Count; i++)
        {
            if (UnitIsInSelectionBox(_camera.WorldToScreenPoint(SelectionManager.Instance.AvilableUnits[i].transform.position), bounds))
            {
                SelectionManager.Instance.Select(SelectionManager.Instance.AvilableUnits[i]);
            }
            else
            {
                SelectionManager.Instance.Deselect(SelectionManager.Instance.AvilableUnits[i]);
            }
        }
    }

    private bool UnitIsInSelectionBox(Vector3 position, Bounds bounds)
    {
        return position.x > bounds.min.x && 
            position.x < bounds.max.x && 
            position.y > bounds.min.y && 
            position.y < bounds.max.y;
    }
}
