using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Users;
using System.Security.Cryptography.X509Certificates;
using TMPro;

public class UIManager : MonoBehaviour
{


    [Header("Canvases")]
    [SerializeField] private GameObject OptionsCanvas;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private GameObject GamepadPanel;
    [SerializeField] private GameObject KeyboardAndMousePanel;

    [Header("PopUps / animations")]
    [SerializeField] private GameObject OverwriteSavePopUp;
    [SerializeField] private GameObject FadeToBlack;
    [SerializeField] private GameObject FadeToGame;


    [Header("First Selected Buttons")]
    public Button settingsPrimaryButton;
    public Button mainPrimaryButton;
    public Button ControlsPrimaryButton;
    public Button OSPopUpPrimaryButton;


    [Header("Other stuff")]
    [SerializeField] private GameObject ContinueButton;
    public bool gameIsPaused;
    [SerializeField] private PlayerInput playerInput;
    PlayerInputActions playerInputActions;
    public bool isMainMenu = false;

    public static bool NewGame = false;

    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString(SaveManager.PlayerCheckpoint, "MainCheckPoint").Equals("MainCheckpoint")
            || PlayerPrefs.GetInt(SaveManager.playerDeathCount, 0) == 0)
        {
            ContinueButton.GetComponent<Button>().enabled = false;
            ContinueButton.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().faceColor = new Color32(255, 255, 255, 40);
            PlayerPrefs.SetString(SaveManager.FirstTimePlaying, "True");

        }
        else
        {
            ContinueButton.GetComponent<Button>().enabled = true;
            ContinueButton.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().faceColor = new Color32(255, 255, 255, 255);
            PlayerPrefs.SetString(SaveManager.FirstTimePlaying, "False");
        }

        Debug.Log("newGame: " + NewGame);
        FadeToBlack.SetActive(false);
        FadeToGame.SetActive(true);

        mainPrimaryButton.Select();
        SettingsPanel.SetActive(false);
        ControlsPanel.SetActive(false);

        //if(first time playing dont show continue button)
    }

    // Update is called once per frame
    void Update()
    {
        ControlSchemeIsChanged();
    }

    public void StartNewGame()
    {
        

        if(PlayerPrefs.GetString(SaveManager.FirstTimePlaying, "True").Equals("True"))
        {
            NewGame = true;
            Debug.Log("newGame: " + NewGame);
            StartCoroutine(LoadGameScene());
        }
        else
        {
            OverwriteSavePopUp.SetActive(true);
            OSPopUpPrimaryButton.Select();
        }

        

    }

    public void DeleteOldSaveData()
    {
        PlayerPrefs.DeleteKey(SaveManager.playerDeathCount);
        PlayerPrefs.DeleteKey(SaveManager.PlayerCheckpoint);

        StartCoroutine(LoadGameScene());
    }

    public void LoadGame()
    {

        StartCoroutine(LoadGameScene());
       
    }

    IEnumerator LoadGameScene()
    {
        
        FadeToBlack.SetActive(true);
        yield return new WaitForSeconds(2);
        LoadSceneManager.instance.StartGame();
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
        if (!isMainMenu)
        {
            Debug.Log("Resume Game");
            gameIsPaused = false;
            OptionsCanvas.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;



            Time.timeScale = 1;
        }
        

    }

    public void PauseGame()
    {
        if (!isMainMenu)
        {
            Debug.Log("show Pause game");
            gameIsPaused = true;
            OptionsCanvas.SetActive(true);
            MainPanel.SetActive(true);
            SettingsPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            mainPrimaryButton.Select();
            SaveManager.instance.SaveData();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;


            Time.timeScale = 0;
        }
    }

}
