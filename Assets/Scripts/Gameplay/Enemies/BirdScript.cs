using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    public GameObject player;
    public Transform startingPoint;
    public bool isChasable = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
             return;      
        if (isChasable == true) 
            ChasePlayer();         
        else
           ReturnToStartingPoint();
        
        Flip();
    }


    private void ChasePlayer()
    {
       transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void ReturnToStartingPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, moveSpeed * Time.deltaTime);
    }
}


