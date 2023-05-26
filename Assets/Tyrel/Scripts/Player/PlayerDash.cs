using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [Header("GroundCheck")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    public float GroundDistance = .5f;

    [Header("PlayerInput")]
    [SerializeField] private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    [Header("DashVariables")]
    [SerializeField] float timer;
    public float DashCooldown = 2;
    [SerializeField] bool isDashinig;
    [SerializeField] bool TouchedGround;
    public float DashAmount = 10;

    private PlayerAudio pAudio;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        pAudio = GetComponent<PlayerAudio>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Dash.performed += Dash;
    }

    private void Update()
    {
        if (isGrounded())
        {
            TouchedGround = true;
        }
;
        if (isDashinig)
        {
            if (timer > 0)
            {
                
                timer -= Time.deltaTime;
            } 
            if (timer <= 0)
            {
                
                isDashinig = false;
                timer = DashCooldown;
                
            }
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashinig && TouchedGround) 
        {
            Debug.Log("Dash");
            pAudio.PlayDash();
            Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

            Vector3 CameraForward = Camera.main.transform.forward;
            Vector3 CameraRight = Camera.main.transform.right;
            CameraForward.y = 0;
            CameraRight.y = 0;
            CameraForward = CameraForward.normalized;
            CameraRight = CameraRight.normalized;

            Vector3 forwardRelativeVerticalInput = InputVector.y * CameraForward;
            Vector3 rightRelativeVerticalInput = InputVector.x * CameraRight;

            Vector3 cameraRelativeMovement = forwardRelativeVerticalInput
                + rightRelativeVerticalInput;
           

            if(InputVector.x > 0.125f || InputVector.y > 0.125f)
            {
                _rb.velocity = cameraRelativeMovement * DashAmount;
            }
            else if(InputVector.x <= 0.125f || InputVector.y <= 0.125f)
            {
                _rb.velocity = CameraForward * DashAmount;
            }



            TouchedGround = false;
            isDashinig = true;
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
