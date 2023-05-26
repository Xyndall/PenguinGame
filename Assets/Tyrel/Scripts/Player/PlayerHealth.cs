using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public int health;

    private int maxHealth = 3;

    public Vector3 VelocityValue;

    bool WillTakeFallDamage;
    int FallDamageAmount;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

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
                Debug.Log("FullHealth" + health);
                break;

            case 2:
                Debug.Log("Two Thirds health" + health);
                break;

            case 1:
                Debug.Log("One Third Health" + health);
                break;

            case 0:
                Debug.Log("Death" + health);
                break;

            default:
                Debug.Log("FullHealth" + health);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (WillTakeFallDamage)
        {
            health -= FallDamageAmount;
        } 

    }


}
