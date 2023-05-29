using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private GameObject OptionsCanvas;


    public bool gameIsPaused;

    PlayerInputActions playerInputActions;

    GameObject player;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += Pause_performed;

        
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {


    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (gameIsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        gameIsPaused = false;
        OptionsCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
        //enable player movement
        //player.GetComponent<PlayerMovement>().enabled = true;
        //player.GetComponent<PlayerDash>().enabled = true;
        //player.GetComponent<PlayerJump>().enabled = true;

    }

    public void PauseGame()
    {
        Debug.Log("show Pause game");
        gameIsPaused = true;
        OptionsCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        Time.timeScale = 0;
        //Disable player movement
        //player.GetComponent<PlayerMovement>().enabled = false;
        //player.GetComponent<PlayerDash>().enabled = false;
        //player.GetComponent<PlayerJump>().enabled = false;

    }

}
