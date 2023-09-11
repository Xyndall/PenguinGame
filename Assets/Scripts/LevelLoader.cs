using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    private void Awake()
    {
        if (instance == null)
        { instance = this; }
    }

    public GameObject loadingScreen;
    public CanvasGroup loadingScreenGroup;
    public Slider slider;
    bool levelLoaded;


    public void LoadLevel(int levelName)
    {
        levelLoaded = false;
        StartCoroutine(LoadAsynchronously(levelName));
        
        
    }

    IEnumerator LoadAsynchronously(int levelName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/ .9f);
           
            slider.value = progress;

            yield return null;
        }
        if(operation.isDone)
        {
            levelLoaded = true;
        }
    }

    private void Update()
    {
        if (levelLoaded)
        {
            loadingScreenGroup.alpha -= 2 * Time.deltaTime; 
            if(loadingScreenGroup.alpha <= 0)
            {
                loadingScreen.SetActive(false);
            }
        }
    }


}
