using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private int direction;

    [SerializeField] private Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        slimeRb = GetComponent<Rigidbody2D>();
        hasTurn = false;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        CheckOnGroundOrWall();
        Movement();
    }


    private void Movement()
    {
        
        slimeRb.velocity = transform.right * moveSpeed;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundPositonChecker.position, new Vector2(groundPositonChecker.position.x, groundPositonChecker.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallPositionChecker.position, new Vector2(wallPositionChecker.position.x + wallCheckDistance, wallPositionChecker.position.y));
    }
}
