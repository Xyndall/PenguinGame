using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string[] SaveDataIntParamNames;
    [SerializeField] private string[] SaveDataFloatParamNames;
    [SerializeField] private string[] SaveDataStringParamNames;


    public static SaveManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        //foreach(string s in SaveDataIntParamNames)
        //{
        //    PlayerPrefs.GetInt(s, 1);
        //}


    }



    public void SaveData()
    {
        PlayerPrefs.Save();
    }

    public void SaveIntData(string paramName, int value)
    {
        PlayerPrefs.SetInt(paramName, value);
    }

    public void SaveFloatData(string paramName, float value)
    {
        PlayerPrefs.SetFloat(paramName, value);
    }

    public void SaveStringData(string paramName, string value)
    {
        PlayerPrefs.SetString(paramName, value);
        Debug.Log("saving: " + paramName + " value: " + value);
    }
}
