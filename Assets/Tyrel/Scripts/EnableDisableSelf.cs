using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableSelf : MonoBehaviour
{
    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
