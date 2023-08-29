using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    PlayerGroundCheck pGroundCheck;

    //use text for now but later health will be shown by the cracking of the egg.
    public TextMeshProUGUI HealthTExt; 

    public int health;

    private int maxHealth = 3;

    public Vector3 VelocityValue;
    public float VelocitymagValue;

    public float minDamageVelocity;
    public float maxDamageVelocity;

    bool WillTakeFallDamage;
    bool WillTakeDamage;
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
        HealthTExt.text = "" + health;
        VelocitymagValue = _rb.velocity.magnitude;
        VelocityValue = _rb.velocity;


        if(health <= 0 )
        {
            Death();
        }

        if(_rb.velocity.y >= -minDamageVelocity)
        {
            FallDamageAmount = 0;
            WillTakeFallDamage = false;
            
        }
        else if (_rb.velocity.y <= -minDamageVelocity && _rb.velocity.y >= -maxDamageVelocity)
        {
            FallDamageAmount = 1;
            WillTakeFallDamage = true;
            
        }
        else if( _rb.velocity.y <= -maxDamageVelocity)
        {
            FallDamageAmount = health + 1;
            WillTakeFallDamage = true;
            
        }


        if (_rb.velocity.magnitude >= maxDamageVelocity)
        {
            
            WillTakeDamage = true;

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
        if (((pGroundCheck.hardGround.value & (1 << collision.gameObject.layer)) != 0) && WillTakeDamage
            || ((pGroundCheck.SoftGround.value & (1 << collision.gameObject.layer)) != 0) && WillTakeDamage)
        {
            health -= 1;
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

        if (other.CompareTag("Death"))
        {
            Death();
        }

    }


    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(1);
        EnemyCanDamage = true;
    }
}
