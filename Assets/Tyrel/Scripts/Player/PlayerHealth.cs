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
    bool wasFalling;
    bool wasGrounded;
    float StartOfFall;
    public float minimumFall;
    public float maximumFall;
    int FallDamageAmount;

    bool EnemyCanDamage;
    bool playerCanDamage;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        pGroundCheck = GetComponent<PlayerGroundCheck>();  

        ResetHealth();
        EnemyCanDamage = true;
        
    }

    bool isFalling { get { return (pGroundCheck.isGrounded() == false && _rb.velocity.y < 0); } }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    private void FixedUpdate()
    {

        if (!wasFalling && isFalling)
        {
            StartOfFall = transform.position.y;
        }
        if (!wasGrounded && pGroundCheck.isGrounded()) 
        {
            float fallDistance = StartOfFall - transform.position.y;

            if(fallDistance >= minimumFall && fallDistance < maximumFall)
            {
                TakeDamage(1);
            }
            else if(fallDistance >= maximumFall)
            {
                TakeDamage(health);
            }
        }

        wasGrounded = pGroundCheck.isGrounded();
        wasFalling = isFalling;
    }

   void ChangeFallDistance()
    {
        if (pGroundCheck.isSoftGround)
        {
            minimumFall = 5;
            maximumFall = 10;
        }
        else if (pGroundCheck.isHardGround)
        {
            minimumFall = 4;
            maximumFall = 8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeFallDistance();
        HealthTExt.text = "" + health;
        VelocitymagValue = _rb.velocity.magnitude;
        VelocityValue = _rb.velocity;

        if(health <= 0 )
        {
            Death();
        }


    }

    public void Death()
    {
        GameManager.instance.PlayerDied();
        GameManager.instance.SpawnAtCurrentCheckpoint(gameObject);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (((pGroundCheck.hardGround.value & (1 << collision.gameObject.layer)) != 0) && WillTakeFallDamage && playerCanDamage)
        //{
        //    playerCanDamage = false;
        //    health -= FallDamageAmount;
        //    Debug.Log("Damage Taken " + FallDamageAmount + ": Remaining Health " + health);
        //}
        //if (((pGroundCheck.SoftGround.value & (1 << collision.gameObject.layer)) != 0) && WillTakeFallDamage && playerCanDamage)
        //{
        //    playerCanDamage = false;
        //    health -= (FallDamageAmount - 1);
        //    Debug.Log("Damage Taken " + FallDamageAmount + ": Remaining Health " + health);
        //}
        //if (((pGroundCheck.hardGround.value & (1 << collision.gameObject.layer)) != 0) && WillTakeDamage && playerCanDamage
        //    || ((pGroundCheck.SoftGround.value & (1 << collision.gameObject.layer)) != 0) && WillTakeDamage && playerCanDamage)
        //{
        //    health -= 1;
        //    playerCanDamage = false;
        //    Debug.Log("Damage Taken " + FallDamageAmount + ": Remaining Health " + health);
        //}

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && EnemyCanDamage)
        {
            TakeDamage(1);
        }

        if (other.CompareTag("Death"))
        {
            Death();
        }

    }

    void TakeDamage(int amount)
    {
        health -= amount;
        EnemyCanDamage = false;
        playerCanDamage = false;
        StartCoroutine(WaitForDamage());
        Debug.Log("Damage Taken " + amount);
        Debug.Log("Player Fell " + (StartOfFall - transform.position.y));
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(1);
        EnemyCanDamage = true;
        playerCanDamage = true;
    }
}
