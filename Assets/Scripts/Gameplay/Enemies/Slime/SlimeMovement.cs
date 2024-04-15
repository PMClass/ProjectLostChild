using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public Transform triangle;
    public GameObject[] wayPoints;
    public int nextWaypoint = 1;
    public float distToWaypoint;
    public float distanceToTurn;
    
    public float moveSpeed;

    public string hasTurn = "hasTurn";
    public Animator animator;

    
    // Update is called once per frame

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
     
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {       
   

        animator.SetBool("hasTurn", false);

        distToWaypoint = Vector2.Distance(transform.position, wayPoints[nextWaypoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWaypoint].transform.position, moveSpeed * Time.deltaTime);

        if(distToWaypoint < distanceToTurn)
        {
            animator.SetBool("hasTurn", true);
            
            TakeTurn();
        }
    }


    private void TakeTurn()
    {
    

        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z += wayPoints[nextWaypoint].transform.eulerAngles.z;
        transform.eulerAngles = currentRotation;

       // wayPoints[nextWaypoint].transform.rotation = Quaternion.Euler(0, 0, -90);
        ChooseNextWaypoint();
        
    }

    private void ChooseNextWaypoint()
    {

        nextWaypoint++;

        if(nextWaypoint == wayPoints.Length)
        {
            nextWaypoint = 0;
        }
    }
}
