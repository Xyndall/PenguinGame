using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private SphereCollider attackCollider;

    // Start is called before the first frame update
    void Start()
    {
        attackCollider.enabled = false;
    }


    public void Attack()
    {
        attackCollider.enabled = true;
        StartCoroutine(TurnOffCollider());
    }

    IEnumerator TurnOffCollider()
    {
        yield return new WaitForSeconds(.5f) ;
        attackCollider.enabled = false;
    }

}
