using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLayer"))
        {
            LoadSceneManager.instance.MainMenu();
        }
    }

}
