using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Users;
using System.Security.Cryptography.X509Certificates;

public class GameManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private GameObject OptionsCanvas;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private GameObject GamepadPanel;
    [SerializeField] private GameObject KeyboardAndMousePanel;

    [Header("First Selected Buttons")]
    public Button settingsPrimaryButton; 
    public Button mainPrimaryButton; 
    public Button ControlsPrimaryButton;




    [Header("Player")]
    public GameObject Player;
    public GameObject[] CheckpointArray;
    public GameObject CurrentCheckpoint;
    [SerializeField] private PlayerInput playerInput;

    public bool gameIsPaused;

    PlayerInputActions playerInputActions;
    string _CurrentControlScheme;


    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += Pause_performed;

        foreach(GameObject checkpoint in CheckpointArray)
        {
            if(checkpoint.GetComponent<Checkpoint>().checkpointName == PlayerPrefs.GetString("Checkpoint", "MainCheckpoint"))
            {
                CurrentCheckpoint = checkpoint;
            }
        }
        
    }

    private void Start()
    {
        SpawnAtCurrentCheckpoint();

        OptionsCanvas.SetActive(false);
    }

    public void SpawnAtCurrentCheckpoint()
    {
        GameObject player = Instantiate(Player, CurrentCheckpoint.GetComponent<Checkpoint>().checkpointSpawn.position, Quaternion.identity);
        playerInput = player.GetComponent<PlayerInput>();
        CinemachineFindPlayer.Instance.SearchForPlayer(player);
    }

    void Update()
    {
        

        ControlSchemeIsChanged();
    }


    void ControlSchemeIsChanged()
    {
        //switches the controls shown on the ui, dependant on what inputs the player is using
        if (playerInput.currentControlScheme == "Gamepad")
        {
            KeyboardAndMousePanel.SetActive(false);
            GamepadPanel.SetActive(true);
        }
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            GamepadPanel.SetActive(false);
            KeyboardAndMousePanel.SetActive(true);
        }
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

    public void SwitchToSettings()
    {
        SettingsPanel.SetActive(true);
        MainPanel.SetActive(false);
        settingsPrimaryButton.Select();
    }

    public void SwitchToMenu()
    {
        SettingsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        MainPanel.SetActive(true);
        mainPrimaryButton.Select();
    }

    public void SwitchToControls()
    {
        MainPanel.SetActive(false);
        ControlsPanel.SetActive(true);
        ControlsPrimaryButton.Select();
        
    }

    //Change player input action map to player
    //playerInput.SwitchCurrentActionMap("Player");
    //

    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        gameIsPaused = false;
        OptionsCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        

        Time.timeScale = 1;

    }

    public void PauseGame()
    {
        Debug.Log("show Pause game");
        gameIsPaused = true;
        OptionsCanvas.SetActive(true);
        MainPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        mainPrimaryButton.Select();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        Time.timeScale = 0;

    }

}
