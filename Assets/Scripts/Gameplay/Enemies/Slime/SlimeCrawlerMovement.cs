using System.Collections;
using System.Collections.Generic;
using System.IO;
using TarodevController;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

public class SlimeCrawlerMovement : MonoBehaviour
{
    private Rigidbody2D slimeRb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private bool onGround, onWall;
    [SerializeField]
    private Transform groundPositonChecker, wallPositionChecker;
    [SerializeField]
    private float groundCheckDistance, wallCheckDistance;
    [SerializeField]
    private LayerMask groundLayer;
    private bool hasTurn;
    [SerializeField]
    private float offset;
    private float zAxisAdd;
    public int direction;

    public PlayerController playerController;
    public PlayerConditions playerConditions;
    public float xForce;
    public float yForce;

    public float maxHitTime;
    private float hitTime;
    public bool IsGoingLeft;

    
    public Transform target;
    [SerializeField] private Animator animator;
    void Start()
    {
       
        animator = GetComponentInChildren<Animator>();
        slimeRb = GetComponent<Rigidbody2D>();
        hasTurn = false;
        playerController = FindObjectOfType<PlayerController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerConditions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConditions>();
        direction = 1;
    }



    private void FixedUpdate()
    {
        CheckOnGroundOrWall();
        Movement();
        hitTime += Time.deltaTime;
    }


    private void Movement()
    {
        if(IsGoingLeft == true)
        {
            slimeRb.velocity = -transform.right * moveSpeed;
        }

        if(IsGoingLeft == false)
        {
            slimeRb.velocity = transform.right * moveSpeed;
        }
    }

    private void CheckOnGroundOrWall()
    {
        onGround = Physics2D.Raycast(groundPositonChecker.position, -transform.up, groundCheckDistance, groundLayer);
        onWall = Physics2D.Raycast(wallPositionChecker.position, transform.right, wallCheckDistance, groundLayer);

        if(!onGround)
        {
            if (hasTurn == false)
            {
              
                zAxisAdd -= 90;
                transform.eulerAngles = new Vector3(0, 0, zAxisAdd);
                if (direction == 1)
                {
                    transform.position = new Vector2(transform.position.x + offset, transform.position.y - offset);
                    hasTurn = true;
                    direction = 2;
                }

                else if(direction == 2)
                {
                    transform.position = new Vector2(transform.position.x - offset, transform.position.y - offset);
                    hasTurn = true;
                    direction = 3;
                }

                else if(direction == 3)
                {
                    transform.position = new Vector2(transform.position.x - offset, transform.position.y + offset);
                    hasTurn = true;
                    direction = 4;
                }

                else if(direction == 4)
                {
                    transform.position = new Vector2(transform.position.x + offset, transform.position.y + offset);
                    hasTurn = true;
                    direction = 1;
                }
            }
        }
        if (onGround)
        {
            hasTurn = false;
        }

        if (onWall)
        {
            if(hasTurn == false)
            {
                zAxisAdd += 90;
                transform.eulerAngles = new Vector3(0, 0, zAxisAdd);

                if(direction == 1)
                {
                    transform.position = new Vector2(transform.position.x + offset, transform.position.y + offset);
                    hasTurn = true;
                    direction = 4;
                }
                else if(direction == 2)
                {
                    transform.position = new Vector2(transform.position.x + offset, transform.position.y - offset);
                    hasTurn = true;
                    direction = 1;
                }
                else if(direction == 3)
                {
                    transform.position = new Vector2(transform.position.x - offset, transform.position.y - offset);
                    hasTurn = true;
                    direction = 2;
                }
                else if(direction == 4)
                {
                    transform.position = new Vector2(transform.position.x - offset, transform.position.y + offset);
                    hasTurn = true;
                    direction = 3;

                }

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundPositonChecker.position, new Vector2(groundPositonChecker.position.x, groundPositonChecker.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallPositionChecker.position, new Vector2(wallPositionChecker.position.x + wallCheckDistance, wallPositionChecker.position.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && hitTime >= maxHitTime)
        {
            if (target.position.x < transform.position.x)
            {
                playerController.AddFrameForce(new(-xForce, yForce), true);
            }
            if(target.position.x >= transform.position.x)
            {
                playerController.AddFrameForce(new(xForce, yForce), true);
            }

            playerConditions.CurrentHealth--;
            if(playerConditions.CurrentHealth <= 0)
            {
                playerConditions.PlayerDie();
            }
            

            hitTime = 0;
        }
    }
}
