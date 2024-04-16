using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTest : MonoBehaviour
{
    //public List<HealthScript> objectsWithHealth = new();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
      

                Debug.Log(collision.gameObject.GetComponentInChildren<Collider2D>());
                HealthScript hs = collision.gameObject.GetComponentInChildren<HealthScript>();
           
                if (hs != null)
                {
                   hs.Damage(1);
                  
                }

                if(hs.health <= 0)
                {
                   hs.Dead();
                   
                }
          

        
    }

    

       
    

    
}
