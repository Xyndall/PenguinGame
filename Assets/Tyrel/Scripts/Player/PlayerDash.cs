using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    PlayerGroundCheck pGroundCheck;
    

    [Header("PlayerInput")]
    [SerializeField] private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    [Header("DashVariables")]
    [SerializeField] float timer;
    public float DashCooldown = 2;
    public float DashAmount = 10;
    [SerializeField] bool isDashinig;
    [SerializeField] bool TouchedGround;
    public GameObject DashEffect;

    private PlayerAudio pAudio;
    public Vector2 vectorShow;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerInput = GameObject.FindGameObjectWithTag("PlayerCameraCinemachine").gameObject.GetComponent<PlayerInput>();
        pAudio = GetComponent<PlayerAudio>();
        pGroundCheck = GetComponent<PlayerGroundCheck>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Dash.performed += Dash;
    }

    private void Update()
    {
        vectorShow = playerInputActions.Player.Move.ReadValue<Vector2>();

        if (pGroundCheck.isGrounded())
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
        //context.performed is to check if the input was performed and to only go through with code once the input is performed
        //inputaction has three states onpressed, performed, released. we check if input has been performed so it doesnt happen every three states.
        if (GameManager.instance.gameIsPaused == false)
        {
            if (context.performed && !isDashinig && TouchedGround)
            {
                //Getting player input and camera position relative to the input
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
                //


                Debug.Log("Dash");
                GameObject particle = Instantiate(DashEffect, pGroundCheck.mainGroundCheck.position, Quaternion.identity);

                Destroy(particle, 0.2f);

                pAudio.PlayDash();


                //if inputs are greater than 0.125f or less then -0.125f (0.125f is the controller stick deadzone)
                //to use camera relative movement for dash direction
                //else   if between -0.125f - 0.125f then use main camera forward for dash direction
                if (InputVector.x > 0.125f || InputVector.y > 0.125f)
                {
                    _rb.velocity = cameraRelativeMovement * DashAmount;
                }
                else if (InputVector.x <= -0.125f || InputVector.y <= -0.125f)
                {
                    _rb.velocity = cameraRelativeMovement * DashAmount;
                }
                else
                {
                    _rb.velocity = CameraForward * DashAmount;
                }



                TouchedGround = false;
                isDashinig = true;
            } 
        }
    }


}
