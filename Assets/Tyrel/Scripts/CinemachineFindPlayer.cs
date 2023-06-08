using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineFindPlayer : MonoBehaviour
{
    public static CinemachineFindPlayer Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private CinemachineFreeLook vCam;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineFreeLook>();
        
    }

    public void SearchForPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        vCam.LookAt = player.transform.GetChild(0).transform;
        vCam.Follow = player.transform.GetChild(0).transform;

        Debug.Log("Camera found " + player.transform.GetChild(0).transform);
    }


}
