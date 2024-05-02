using System;
using System.Collections;
using UnityEngine;

public class TurretBulletBehavior : MonoBehaviour
{
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

        Destroy(this.gameObject);
    }
}
