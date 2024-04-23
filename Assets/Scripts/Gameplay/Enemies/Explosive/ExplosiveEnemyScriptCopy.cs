using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class ExplosiveEnemyScript : MonoBehaviour
{
    
    private Transform target;
    public float speed;
    private float direction;
    float distToPlayer;
    bool canChase;
    public float chaseDistance = 5f;
    public float stopChasingDistance = 10f;

    public float distanceToExplode;
    public float explosiveTime;
    public float explosiveTicks;
    public float explosionDuration;
    public bool canExplode;

    public float maxTime;
    public float movingTime;
    Rigidbody2D rb;

    bool facingRight = true;
    public SpriteRenderer bombSprite;
    public Color originalColor;
    public Color turnRed;


    bool notExploding = true;
    public float fieldOfImpact;
    public float forcePushBack;
    public LayerMask layerToHit;

    public Animator animator;

    public PlayerController playerController;

    public float xForce;
    public float yForce;

    public float maxHitTime;
    private float hitTime;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bombSprite = GetComponentInChildren<SpriteRenderer>();
        originalColor = bombSprite.color;
        canExplode = false;     

        playerController = FindObjectOfType<PlayerController>();

    }
    void FixedUpdate()
    {

        if (Vector2.Distance(transform.position, target.position) < chaseDistance)
        {
            canChase = true;
            animator.SetBool("chasePlayer", true);
        }

        if (Vector2.Distance(transform.position, target.position) > stopChasingDistance)
        {
            canChase = false;
            animator.SetBool("chasePlayer", false);
        }

        if (canChase)
        {
            float moveHorizontal = direction;

            Vector3 movement = new Vector3(moveHorizontal, 0.0f);
            rb.velocity = movement * speed;

            
        }

       

        distToPlayer = Vector2.Distance(transform.position, target.position);
       
        // Direction is Right   
        if (target.position.x > transform.position.x)
        {
            direction = 1;
            if (!facingRight && notExploding)
            {
                Flip();
            }       
          
        }

            
        // Direction is left
        if (target.position.x < transform.position.x)
        {
            direction = -1;
            if (facingRight && notExploding)
            {
                Flip();
            }          
                
        }

        if (distToPlayer <= distanceToExplode)
        {
            animator.SetBool("chasePlayer", false);          
            StartCoroutine(Explode());
        }

        hitTime += Time.deltaTime;

        Physics2D.IgnoreLayerCollision(11, 12);
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

   
    private void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);

        foreach(Collider2D obj in objects)
        {
           // Vector2 direction = obj.transform.position - transform.position;

            if(obj.tag == "Player")
            {
                if (facingRight)
                {
                    playerController.AddFrameForce(new(xForce, yForce), true);
                 
                }

                else
                {
                    playerController.AddFrameForce(new(-xForce, yForce), true);
                }
              
            }
        }
    }


    IEnumerator Explode()
    {
        
        notExploding = false;
        movingTime += Time.deltaTime;

      
        rb.mass = 10;
        if (transform.position.x > target.position.x)
        {
            facingRight = false;
        }
        if(transform.position.x < target.position.x)
        {
            facingRight = true;
        }

        if (movingTime >= maxTime)
        {
            animator.SetTrigger("explode");
            for (int i = 0; i < explosiveTicks; i++)
            {

                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                movingTime = 0;
                bombSprite.color = turnRed;     
                yield return new WaitForSeconds(explosiveTime);
                bombSprite.color = originalColor;
                yield return new WaitForSeconds(explosiveTime);
            }           
            
            canExplode = true;
            
        }
        if (canExplode)
        {
          
            bombSprite.color = turnRed;
            Explosion();
            yield return new WaitForSeconds(0.75f);
            bombSprite.color = turnRed;
            Destroy(gameObject);
        }
       

    }

    private void OnDrawGizmosSelected()
    {
        // Explosion Radius Drawn Out
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);

        // Distance Check to Explode
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToExplode);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && hitTime >= maxHitTime)
        {
            if (facingRight)
            {
                playerController.AddFrameForce(new(5, 5), true);
            }
            else
            {
                playerController.AddFrameForce(new(-5, 5), true);
            }

            hitTime = 0;
        }

        if(collision.gameObject.tag == "ExplosiveEnemy")
        {
            Physics2D.IgnoreLayerCollision(11, 11);
        }

        

    }


}