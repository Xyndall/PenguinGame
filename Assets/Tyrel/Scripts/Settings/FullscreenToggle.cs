using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    public Toggle _toggle;

    
    private void Awake()
    {
        

        int isFullsreen = PlayerPrefs.GetInt("Fullscreen", 1);
        //Debug.Log("Fullscreen toggle retrieved " + isFullsreen);
        if (isFullsreen == 1)
        {
            Change(true);
        }
        else
        {
            Change(false);
        }

        

    }


    public void Change(bool on)
    {
        //Debug.Log("Fullscreen toggle saved " + on);

        _toggle.isOn = on;
        Screen.fullScreenMode = on ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        if (on)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 3);
        }
        
    }
}
