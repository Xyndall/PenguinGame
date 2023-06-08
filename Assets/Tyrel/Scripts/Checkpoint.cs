using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string checkpointName;

    public Transform checkpointSpawn;
    private bool AlreadyEntered = false;

    [SerializeField] private AudioSource aSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !AlreadyEntered)
        {
            //aSource.Play();
            other.GetComponent<PlayerCheckpoint>().SetNewCheckpoint(gameObject);
            AlreadyEntered = true;
            //GameManager.instance.lastPlayerCheckpoint = this.gameObject;
            SaveManager.instance.SaveStringData("Checkpoint", checkpointName);
        }
    }
}
