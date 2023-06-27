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
    public GameObject JumpEffect;

    [Header("GroundCheck")]
    PlayerGroundCheck pGroundCheck;
    [SerializeField] private PlayerInput playerInput;

    private PlayerAudio pAudio;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        pAudio = GetComponent<PlayerAudio>();
        pGroundCheck = GetComponent<PlayerGroundCheck>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
    }



    public void Jump(InputAction.CallbackContext context)
    {
        if (GameManager.instance.gameIsPaused == false)
        {
            if (context.performed && pGroundCheck.isGrounded())
            {
                GameObject particle = Instantiate(JumpEffect, pGroundCheck.mainGroundCheck.position, Quaternion.identity);
                Destroy(particle, 0.2f);
                pAudio.PlayJump();
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            } 
        }
    }

    
}
