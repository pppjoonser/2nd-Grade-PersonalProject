using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static KeyAction;

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    [SerializeField]
    private LayerMask WhatIsGround;
    private KeyAction _playerInput;

    public event Action OnClick, OnCancleClick;
    public event Action<Vector2> OnMoveAction;
    public event Action OnMoveOrder;
    public event Action OnInventoryPressed;
    public event Action<bool> OnPressedCameraRotate;
    public event Action<float> OnUpDownCameraPressed;

    private Vector2 _screenPosition;
    private Vector3 _worldPosition;

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

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            HoldingMultiSelect = true;
        else if (context.canceled)
            HoldingMultiSelect = false;
    }

    public Vector3 OnPointer(InputAction.CallbackContext context)
    {
        Camera mainCam = Camera.main;
        Debug.Assert(mainCam != null, "No main camera in this scene");
        Ray camRay = mainCam.ScreenPointToRay(_screenPosition);
        if(Physics.Raycast(camRay, out RaycastHit hit , mainCam.farClipPlane, WhatIsGround))
        {
            _worldPosition = hit.point;
        }

        return _worldPosition;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInventoryPressed?.Invoke();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<float>();
        OnUpDownCameraPressed?.Invoke(val);
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnPressedCameraRotate?.Invoke(true);
        else if(context.canceled)
            OnPressedCameraRotate?.Invoke(false);
    }
}
