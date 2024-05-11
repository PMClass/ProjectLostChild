using System.Collections;
using TarodevController;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BoarChasePlayer : MonoBehaviour
{
    AudioSource boarSounds;
    public AudioClip boarImpact;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float roamingSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private int patrolDestination = 0;
    [SerializeField] public Transform[] patrolPoints;
  

    public Transform playerTransform;
    public Transform hitDistanceCheck;
    public float chaseDistance;
    public float hitDistance;

    public Rigidbody2D rb;
    public Animator anim;
    

    public bool facingRight;
    public bool canMove;

    public enum State
    {
        Roaming,
        ChasingPlayer,
        Attacking
    }

    public State state;

    public float hitMaxTime = 3;
    public float nextHitTime = 0;

    public PlayerConditions playerConditions;
    public PlayerController playerController;
    public GameManager gm;
    public GameObject playerChar;
    private void Awake()
    {
        gm = GameManager.Instance;
        

       
    }

    public IEnumerator Start()
    {
        enabled = false;

        while(gm.CurrentState != GameManager.GMState.GAME)
        {
            yield return null;
        }

        moveSpeed = roamingSpeed;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        facingRight = false;
        canMove = true;

        state = State.Roaming;

        playerChar = gm.CurrentPlayer;
        playerController = playerChar.GetComponent<PlayerController>();
        playerTransform = playerChar.transform;
        playerConditions = playerChar.GetComponent<PlayerConditions>();

        enabled = true;
    }

    private void FixedUpdate()
    {


        nextHitTime += Time.deltaTime;

        switch (state)
        {
            default:
               
            case State.Roaming:
                if (canMove)
                {
                    moveSpeed = roamingSpeed;



                    // Going to the left
                    if (patrolDestination == 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                        if (facingRight)
                        {
                            Flip();
                        }
                        if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                        {
                            patrolDestination = 1;

                        }
                    }

                    // Going to the right
                    if (patrolDestination == 1)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                        if (!facingRight)
                        {
                            Flip();
                        }
                        if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
                        {
                            patrolDestination = 0;

                        }
                    }

                    // Is the player in chase distance?
                    if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
                    {
                        state = State.ChasingPlayer;
                    }
                }
                break;


            case State.ChasingPlayer:

                if (canMove)
                {
                    // Player is not in range to be chased
                    if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
                    {
                        state = State.Roaming;
                    }

                    // Increase move speed
                    moveSpeed += Time.deltaTime;
                    if (moveSpeed >= maxSpeed)
                    {
                        moveSpeed = maxSpeed;
                    }

                    // If the player is on the left side of the boar, go left
                    if (playerTransform.position.x < transform.position.x)
                    {
                        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                        if (facingRight)
                        {
                            Flip();
                        }

                    }

                    // If the player is on the right side of the boar, chase right
                    if (playerTransform.position.x > transform.position.x)
                    {
                        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                        if (!facingRight)
                        {
                            Flip();
                        }

                    }

                    // Is the player in hit range
                    if (Vector2.Distance(hitDistanceCheck.position, playerTransform.position) < hitDistance)
                    {
                        canMove = false;
                        state = State.Attacking;
                        
                    }

                }
                break;
            case State.Attacking:

                
                if (nextHitTime > hitMaxTime)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    StartCoroutine(Attack());
                    nextHitTime = 0;
                }
         
                break;


        }    

    
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    IEnumerator Attack()
    {
        
        anim.SetBool("canAttack", true);
        
        yield return new WaitForSeconds(2f);
        
        anim.SetBool("canAttack", false);
        canMove = true;
        state = State.ChasingPlayer;

    }

 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && state == State.Attacking)
        {
            boarSounds.PlayOneShot(boarImpact);
            playerController.AddFrameForce(new(8, 8), true);
            playerConditions.PlayerGiveHurt();
           
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(hitDistanceCheck.position, playerTransform.position);
    }


}
