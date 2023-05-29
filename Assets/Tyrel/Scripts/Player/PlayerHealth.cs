using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    PlayerGroundCheck pGroundCheck;

    public int health;

    private int maxHealth = 3;

    public Vector3 VelocityValue;

    bool WillTakeFallDamage;
    int FallDamageAmount;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        pGroundCheck = GetComponent<PlayerGroundCheck>();  

        ResetHealth();
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        VelocityValue = _rb.velocity;


        switch (health)
        {
            case 3:
                
                break;

            case 2:
                
                break;

            case 1:
                
                break;

            case 0:
                
                break;

            case -1:
                
                break;

            default:
                
                break;
        }

        if(_rb.velocity.y >= -5)
        {
            WillTakeFallDamage = false;
            FallDamageAmount = 0;
        }
        else if (_rb.velocity.y <= -5 && _rb.velocity.y >= -10)
        {
            WillTakeFallDamage = true;
            FallDamageAmount = 1;
        }
        else if( _rb.velocity.y <= -10)
        {
            WillTakeFallDamage = true;
            FallDamageAmount = health;
        }
        

    }

    public void Death()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((pGroundCheck.hardGround.value & (1 << collision.gameObject.layer)) != 0) && WillTakeFallDamage)
        {
            health -= FallDamageAmount;
            Debug.Log("Damage Taken " + FallDamageAmount + ": Remaining Health " + health);
        }
        if (((pGroundCheck.SoftGround.value & (1 << collision.gameObject.layer)) != 0) && WillTakeFallDamage)
        {
            health -= (FallDamageAmount - 1);
            Debug.Log("Damage Taken " + FallDamageAmount + ": Remaining Health " + health);
        }


    }


}