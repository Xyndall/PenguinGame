using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    public float GroundDistance = .5f;

    [SerializeField] private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    float timer;
    int dashCounter = 1;
    bool isDashinig;
    float DashAmount = 10;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Dash.performed += Dash;
    }

    private void Update()
    {

        if (isDashinig)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            } 
            if (timer <= 0)
            {
                isDashinig = false;
                timer = 2;
            }
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && !isDashinig)
        {
            Debug.Log("Dash");

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
            _rb.AddForce(cameraRelativeMovement.normalized * DashAmount * Time.deltaTime, ForceMode.Impulse);


            
            isDashinig = true;
            dashCounter--;
        }
    }

    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, -Vector3.up * GroundDistance, out hit, 0.3f, ground))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
