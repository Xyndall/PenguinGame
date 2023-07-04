using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string collectableName;
    private void Start()
    {
        if(PlayerPrefs.GetString(collectableName, "False").Equals("True"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int collectables = PlayerPrefs.GetInt(SaveManager.CollectablesCount, 0);
            collectables += 1;
            PlayerPrefs.SetInt(SaveManager.CollectablesCount, collectables);
            PlayerPrefs.SetString(collectableName, "True");
            Destroy(gameObject);

        }
    }

}
