using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{

    public CinemachineFreeLook playerCamera;

    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider.GetComponent<Slider>();
        float speed = PlayerPrefs.GetFloat("MouseSensitivity", 300);
        _slider.value = speed;
        ChangeSensitivity(speed);
    }

    public void ChangeSensitivity(float speed)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", speed);
        playerCamera.m_XAxis.m_MaxSpeed = speed;

    }


}
