using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [Header("JumpVariables")]
    public float jumpForce = 5f;
    public float GroundDistance = .5f;

    [Header("GrounbdCheck")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] private PlayerInput playerInput;

    private PlayerAudio pAudio;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        pAudio = GetComponent<PlayerAudio>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
    }

    private void Update()
    {

        Debug.DrawRay(groundCheck.position, -Vector3.up * GroundDistance, Color.red);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            pAudio.PlayJump();
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
