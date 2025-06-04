using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private InputReader _input;

    private Vector3 cameramove;
    private float updounInput;
    private bool _wheelButtonPressed = false;
    private void Awake()
    {
        _input.OnPressedCameraRotate += HandleCameraRotatePressed;
        _input.OnUpDownCameraPressed += HandleUpDownMove;
        _input.OnMoveAction += HandleMoveInput;
    }

    private void HandleMoveInput(Vector2 vector)
    {
        cameramove.x = vector.x;
        cameramove.z = vector.y;
    }
    private void HandleUpDownMove(float obj)
    {
        cameramove.y = obj;
    }
    private void HandleCameraRotatePressed(bool val)
    {
        _wheelButtonPressed = val;
    }

    private void Update()
    {
        MoveCamera( cameramove );
        if( _wheelButtonPressed )
        {
            RotateCamera(Mouse.current.delta.value);
        }
    }
    public void RotateCamera(Vector3 mouseDelta)
    {
        transform.Rotate(Vector3.up, -mouseDelta.x * rotationSpeed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.right, mouseDelta.y * rotationSpeed * Time.deltaTime, Space.Self);
    }
    // Update is called once per frame

    public void MoveCamera(Vector3 inputDirection)
    {
        Vector3 normalizedDirection = inputDirection.normalized;

        transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime, Space.Self);
    }
}
