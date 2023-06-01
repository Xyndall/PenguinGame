using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer = null;
    public string ParamName;

    [SerializeField] private Slider _slider;

    void Start()
    {
        _slider.GetComponent<Slider>();
        float vol = PlayerPrefs.GetFloat(ParamName, 1);
        _slider.value = vol;
        SetVolume(vol);
    }

    public void SetVolume(float Value)
    {
        PlayerPrefs.SetFloat(ParamName, Value);
        Value *= 100;
        Value -= 100;


        audioMixer.SetFloat(ParamName, Value);
    }
}
