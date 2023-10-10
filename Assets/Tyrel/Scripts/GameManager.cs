using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Users;
using System.Security.Cryptography.X509Certificates;
using TMPro;

public class GameManager : MonoBehaviour
{


    [Header("Player")]
    public GameObject Player;
    public GameObject[] CheckpointArray;
    public GameObject CurrentCheckpoint;
    GameObject LastPlayerCurrent;
    public TextMeshProUGUI CollectablesText;


    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        

        foreach(GameObject checkpoint in CheckpointArray)
        {
            if(checkpoint.GetComponent<Checkpoint>().checkpointName == PlayerPrefs.GetString(SaveManager.PlayerCheckpoint, "MainCheckpoint"))
            {
                CurrentCheckpoint = checkpoint;
            }
        }
        
    }

    private void Start()
    {
        SpawnAtCurrentCheckpoint();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CollectablesText.text = PlayerPrefs.GetInt(SaveManager.CollectablesCount, 0).ToString();
    }

    public void SetCurrentPlayerGO(GameObject player)
    {
        LastPlayerCurrent = player;
    }

    public void SpawnAtCurrentCheckpoint()
    {

        if(LastPlayerCurrent != null)
        {
            Destroy(LastPlayerCurrent);
        }
        GameObject player = Instantiate(Player, CurrentCheckpoint.GetComponent<Checkpoint>().checkpointSpawn.position, Quaternion.identity);
        CinemachineFindPlayer.Instance.SearchForPlayer(player);
        LastPlayerCurrent = player;
    }


    public void PlayerDied()
    {
        int playerdeaths = PlayerPrefs.GetInt(SaveManager.playerDeathCount, 0);
        playerdeaths += 1;
        PlayerPrefs.SetInt(SaveManager.playerDeathCount, playerdeaths);
    }
    

    

    

    

    

}
