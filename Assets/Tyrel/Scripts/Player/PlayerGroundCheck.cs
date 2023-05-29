using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [Header("GroundCheck")]
    public Transform groundCheck;
    public LayerMask ground, hardGround, SoftGround;
    public float GroundDistance = .5f;



    void Start()
    {
        
    }


     void Update()
    {

        Debug.DrawRay(groundCheck.position, -Vector3.up * GroundDistance, Color.red);
    }

    public bool isGrounded()
    {
        //bool returns true if raycast is hitting layermask ground else returns false
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, -Vector3.up * GroundDistance, out hit, 0.3f, ground))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
