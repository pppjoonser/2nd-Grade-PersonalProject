using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static KeyAction;

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    private KeyAction _playerInput;
    public event Action OnClick, OnCancleClick;
    public event Action<Vector2> OnMoveAction;
    public event Action OnMoveOrder;
    public bool HoldingSelect { get; private set; } = false;
    public bool HoldingMultiSelect {  get; private set; } = false;

    private void OnEnable()
    {
        if(_playerInput == null)
        {
            _playerInput = new KeyAction();
            _playerInput.Player.SetCallbacks(this);
        }
        _playerInput.Enable();

        HoldingSelect = false;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnClick?.Invoke(); 
            HoldingSelect = true; 
        }
        else if (context.canceled)
        {
            OnCancleClick?.Invoke();
            HoldingSelect = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnMoveOrder?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 val = context.ReadValue<Vector2>();
        OnMoveAction?.Invoke(val);
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            HoldingMultiSelect = true;
        else if (context.canceled)
            HoldingMultiSelect = false;
    }

}
