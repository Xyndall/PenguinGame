using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();

    }


    IEnumerator WaitForSave()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }

}
