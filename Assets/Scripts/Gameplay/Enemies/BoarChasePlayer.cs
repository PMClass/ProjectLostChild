using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoarChasePlayer : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private int patrolDestination = 0;
    [SerializeField] public Transform[] patrolPoints;
    public bool isPatrolling;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    public float stopChasingDistance;



    private void Awake()
    {
        isChasing = false;
        //isPatrolling = false;
      
    }

    private void FixedUpdate()
    {
        if (isChasing)
        {
            moveSpeed += Time.deltaTime;

            if(moveSpeed >= 3)
            {
                moveSpeed = 3f;
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

            // When the player is far enough from the boar, stop chasing
           /* if (Vector2.Distance(transform.position, playerTransform.position) > stopChasingDistance)
            {
                isChasing = false;
                
               
            } */
        }

        else
        {
            // If the player is close to the boar, chase player
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }


        }

        // Movement
        if (!isChasing)
        {
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
        }

        /* if (!isChasing)
         {
             if (Vector2.Distance(transform.position, patrolPoints[0].position) < Vector2.Distance(transform.position, patrolPoints[1].position))
             {

                 transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
             }
             else
             {
                 transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
             }
         }
        */



    }


}
