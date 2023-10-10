using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ResolutionSetter : MonoBehaviour
{
    public TMP_Dropdown _dropdown;

    private void Awake()
    {
        
        _dropdown.options.Clear();
        Resolution[] resolutionArray = Screen.resolutions;


        int length = resolutionArray.Length;
        for (int i = 0; i < length; i++)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData(resolutionArray[i].ToString());
            _dropdown.options.Add(data);
            if (resolutionArray[i].Equals(Screen.currentResolution))
            {
                int ResolutionSet = PlayerPrefs.GetInt("ResolutionSettings", i);
                //Debug.Log("Resolution settings int retrieved : " + ResolutionSet);
                ChangeResolution(ResolutionSet);

                

            }

        }

        
    }


    public void ChangeResolution(int value)
    {
        //Debug.Log("Resolution settings saved " + value);

        Resolution res = Screen.resolutions[value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        _dropdown.value = value;
        PlayerPrefs.SetInt("ResolutionSettings", value);
    }
}
