using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;

public class BoarChasePlayer : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private int patrolDestination = 0;
    [SerializeField] public Transform[] patrolPoints;
  

    public Transform playerTransform;
  
    public float chaseDistance;
    public float hitDistance;
    public Rigidbody2D rb;
    

    public enum State
    {
        Roaming,
        ChasingPlayer,
        Attacking
    }

    public State state;

    public PlayerController playerController;

    public CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
       
      
        moveSpeed = minSpeed;
        rb = GetComponent<Rigidbody2D>();
        state = State.Roaming;
        playerController = FindObjectOfType<PlayerController>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
      
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                moveSpeed = minSpeed;

                if (patrolDestination == 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                    {
                        patrolDestination = 1;

                    }
                }

                if (patrolDestination == 1)
                {
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
                    {
                        patrolDestination = 0;

                    }
                }

                if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
                {
                    state = State.ChasingPlayer;

                }

                break;


            case State.ChasingPlayer:
               
                if(Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
                {
                    state = State.Roaming;
                }

               
                moveSpeed += Time.deltaTime;

                if (moveSpeed >= maxSpeed)
                {
                    moveSpeed = maxSpeed;
                }

                // If the player is on the left side of the boar, go left
                if (transform.position.x > playerTransform.position.x)
                {
                    transform.position += Vector3.left * moveSpeed * Time.deltaTime;


                }

                // If the player is on the right side of the boar, chase right
                if (transform.position.x < playerTransform.position.x)
                {
                    transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                }

                if(Vector2.Distance(transform.position, playerTransform.position) < hitDistance)
                {
                    state = State.Attacking;
                }
              
                break;
            case State.Attacking:

                StartCoroutine(Attack());
               
                if(Vector2.Distance(transform.position, playerTransform.position) > hitDistance)
                {
                    state = State.ChasingPlayer;
                }

                break;


        }


        Debug.Log(state);
       

       
      

     

       
        

    
    }

    IEnumerator Attack()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1);

    }

    private void AttackPlayer()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    

}
