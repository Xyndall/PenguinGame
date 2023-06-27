using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform camFollow;
    [SerializeField] private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    public float _moveSpeed = 5f;


    PlayerGroundCheck pGroundCheck;

    private PlayerAudio pAudio;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerInput = GameObject.FindGameObjectWithTag("PlayerCameraCinemachine").gameObject.GetComponent<PlayerInput>();
        pAudio = GetComponent<PlayerAudio>();
        pGroundCheck = GetComponent<PlayerGroundCheck>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameIsPaused == false)
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
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if((pGroundCheck.hardGround.value & (1 << collision.gameObject.layer)) != 0)
        {
            //Debug.Log("HardCollided");
            pAudio.PlayHardCollision();
        }

        if((pGroundCheck.SoftGround.value & (1 << collision.gameObject.layer)) != 0)
        {
            //Debug.Log("softCollided");
            pAudio.PlaySoftCollision();
        }

    }


}
