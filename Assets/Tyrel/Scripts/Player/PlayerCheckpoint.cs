using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public GameObject LastCheckpoint;

    //gets called when player enters checkpoint trigger
    //sets last checkpoint to the new checkpoint trigger player touched
    public void SetNewCheckpoint(GameObject checkPoint)
    {
        Debug.Log("Set new Checkpoint");
        LastCheckpoint = checkPoint;
    }


    public void GoToLastCheckpoint()
    {


        //Debug.Log("GoingToLastCheckpoint");

        //gameObject.transform.position = LastCheckpoint.GetComponent<Checkpoint>().checkpointSpawn.position;

        //// need to make player not carry velocity when resetting
        //// its dependant on how you made the movement,
        //// this should work but you might need to change a few things like swapping the isKinematic's around
        //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //StartCoroutine(ResetPlayer());
    }

    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
