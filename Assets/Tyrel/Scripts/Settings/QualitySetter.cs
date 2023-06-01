using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualitySetter : MonoBehaviour
{
    public TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdown.options.Clear();
        string[] qualityLevels = QualitySettings.names;

        for (int i = 0; i < qualityLevels.Length; i++)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData(qualityLevels[i]);
            _dropdown.options.Add(data);
        }

        int QualitySet = PlayerPrefs.GetInt("QualitySettings", QualitySettings.GetQualityLevel());
        //Debug.Log("Quality settings int retrieved : " +  QualitySet);


        ChangeQuality(QualitySet);

    }

    public void ChangeQuality(int value)
    {
        //Debug.Log("Quality settings saved " + value);

        _dropdown.value = value;
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt("QualitySettings", value);
    }
}
