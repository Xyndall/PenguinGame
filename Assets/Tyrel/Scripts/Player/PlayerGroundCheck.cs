using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [Header("GroundCheck")]
    public Transform mainGroundCheck;
    public LayerMask ground, hardGround, SoftGround;
    float GroundDistance = 0.5f;
    float maxDistance = 0.3f;

    public Transform[] raycastGroundChecks;


    public bool isGrounded()
    {
        //bool returns true if raycast is hitting layermask ground else returns false
        RaycastHit hit;
        if (Physics.Raycast(mainGroundCheck.position, -Vector3.up * GroundDistance, out hit, maxDistance, ground) 
            || Physics.Raycast(raycastGroundChecks[0].position, -Vector3.up * GroundDistance, out hit, maxDistance, ground)
            || Physics.Raycast(raycastGroundChecks[1].position, -Vector3.up * GroundDistance, out hit, maxDistance, ground)
            || Physics.Raycast(raycastGroundChecks[2].position, -Vector3.up * GroundDistance, out hit, maxDistance, ground)
            || Physics.Raycast(raycastGroundChecks[3].position, -Vector3.up * GroundDistance, out hit, maxDistance, ground))
        {
            return true;
        }
        else
        {
            return false;
        }




    }
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(mainGroundCheck.position, -Vector3.up * GroundDistance, Color.red);
        Debug.DrawRay(raycastGroundChecks[0].position, -Vector3.up * GroundDistance, Color.red);
        Debug.DrawRay(raycastGroundChecks[1].position, -Vector3.up * GroundDistance, Color.red);
        Debug.DrawRay(raycastGroundChecks[2].position, -Vector3.up * GroundDistance, Color.red);
        Debug.DrawRay(raycastGroundChecks[3].position, -Vector3.up * GroundDistance, Color.red);
        

    }
}
