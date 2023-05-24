using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public float _moveSpeed = 5f;

    [SerializeField] private Transform camFollow;
    [SerializeField] private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = InputVector.y * forward;
        Vector3 rightRelativeVerticalInput = InputVector.x * right;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput
            + rightRelativeVerticalInput;
        _rb.AddForce(cameraRelativeMovement * _moveSpeed * Time.deltaTime, ForceMode.Impulse);


       // _rb.AddForce(new Vector3(InputVector.x, 0, InputVector.y) * _moveSpeed * Time.deltaTime, ForceMode.Impulse);
    }
}
