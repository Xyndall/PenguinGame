using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Win : MonoBehaviour
{
    public GameObject WinScreen;

    private void Start()
    {
        WinScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinScreen.SetActive(true);
            StartCoroutine(ShowWinScreen());
        }
    }

    IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(10);
        LoadSceneManager.instance.MainMenu();
    }

}
