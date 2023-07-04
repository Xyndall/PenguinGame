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

    bool EnemyCanDamage;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        pGroundCheck = GetComponent<PlayerGroundCheck>();  

        ResetHealth();
        EnemyCanDamage = true;
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        VelocityValue = _rb.velocity;


        if(health <= 0 )
        {
            Death();
        }

        if(_rb.velocity.y >= -8)
        {
            WillTakeFallDamage = false;
            FallDamageAmount = 0;
        }
        else if (_rb.velocity.y <= -8 && _rb.velocity.y >= -15)
        {
            WillTakeFallDamage = true;
            FallDamageAmount = 1;
        }
        else if( _rb.velocity.y <= -15)
        {
            WillTakeFallDamage = true;
            FallDamageAmount = health;
        }
        

    }

    public void Death()
    {
        GameManager.instance.PlayerDied();
        GameManager.instance.SpawnAtCurrentCheckpoint(gameObject);
        
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && EnemyCanDamage)
        {
            health -= 1;
            EnemyCanDamage = false;
            Debug.Log("Damage taken : 1");
            StartCoroutine(WaitForDamage());
        }
    }


    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(1);
        EnemyCanDamage = true;
    }
}
