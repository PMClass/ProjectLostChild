using System;
using System.Collections;
using TarodevController;
using UnityEngine;

public class TurretBulletBehavior : MonoBehaviour
{
    PlayerController playerController;
    PlayerConditions playerConditions;
    public float xForce = 1, yForce = 1;
    private void Awake()
    {
        playerConditions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConditions>();
        playerController = FindObjectOfType<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnCollisionEnteR2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Companion")
        {
            
        }

        if(other.gameObject.tag == "Player")
        {
            playerController.AddFrameForce(new(xForce, yForce), true);
            playerConditions.CurrentHealth--;

            if(playerConditions.CurrentHealth <= 0)
            {
                playerConditions.PlayerDie();
            }
        }

        

        Destroy(this.gameObject);
    }
}
