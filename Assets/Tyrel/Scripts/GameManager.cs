using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private GameObject OptionsCanvas;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject SettingsPanel;
    public Button settingsPrimaryButton; 
    public Button mainPrimaryButton; 

    [Header("Player")]
    [SerializeField] private PlayerInput playerInput;

    public bool gameIsPaused;

    PlayerInputActions playerInputActions;



    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += Pause_performed;

    }

    private void Start()
    {
        

        OptionsCanvas.SetActive(false);
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

    public void SwitchToSettings()
    {
        SettingsPanel.SetActive(true);
        MainPanel.SetActive(false);
        settingsPrimaryButton.Select();
    }

    public void SwitchToMenu()
    {
        SettingsPanel.SetActive(false);
        MainPanel.SetActive(true);
        mainPrimaryButton.Select();
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
        mainPrimaryButton.Select();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        Time.timeScale = 0;

    }

}
