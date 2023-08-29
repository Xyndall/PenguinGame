using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAI : MonoBehaviour
{
    [SerializeField]private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("Trying to hit player");
            float force = 10f;

            animator.Play("Idle_C");

            // Calculate Angle Between the collision point and the player
            Vector3 dir = other.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            other.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
        }
    }

}
