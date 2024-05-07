using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class BoarTrigger : MonoBehaviour
{

    public PlayerController playerController;
    [SerializeField] private float xForce;
    [SerializeField] private float yForce;
  

    private Transform playerTransform;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       
    }

    private void FixedUpdate()
    {
        
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
                if (playerTransform.position.x < this.transform.position.x) playerController.AddFrameForce(new(-xForce, yForce), true);

                if (playerTransform.position.x > this.transform.position.x) playerController.AddFrameForce(new(xForce, yForce), true);
            

           

        }
    }
}
