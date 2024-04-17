using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class ExplosiveEnemyScript : MonoBehaviour
{
    
    private Transform target;
    public float speed;
    private float direction;
    float distToPlayer;

    public float distanceToExplode;
    public float explosiveTime;
    public float explosiveTicks;
  

    public float maxTime;
    public float movingTime;
    Rigidbody2D rb;

    SpriteRenderer sp;
    public Color originalColor;
    public Color turnRed;

    public GameObject explosion;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sp = GetComponent<SpriteRenderer>();
        originalColor = sp.color;
    }
    void Update()
    {

        float moveHorizontal = direction;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f);
        rb.velocity = movement * speed;
        distToPlayer = Vector2.Distance(transform.position,target.position);
        Debug.Log(movement.x +  ", " + movement.y);
       //Debug.Log(distToPlayer);
           
        if (target.position.x > transform.position.x)
        {
                
                direction = 1;

            while(explosion.transform.localScale.x < 2)
            {
                explosion.transform.localScale += new Vector3(0.1f, 0.1f) * Time.deltaTime;
            }
                
            if(distToPlayer <= distanceToExplode)
            {                           
                StartCoroutine(Explode());
              
            }
           /* else
            {
                sp.color = originalColor;
            } */

        }

            
        
        if (target.position.x < transform.position.x)
            {

                direction = -1;
                 
            if(distToPlayer <= distanceToExplode) { sp.color = turnRed; }
                

            }

           
        
        
    }

   

    IEnumerator Explode()
    {

        movingTime += Time.deltaTime;
        if (movingTime >= maxTime)
        {
            for (int i = 0; i < explosiveTicks; i++)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                movingTime = 0;
                sp.color = turnRed;     
                yield return new WaitForSeconds(explosiveTime);
                sp.color = originalColor;
                yield return new WaitForSeconds(explosiveTime);
            }
            sp.color = turnRed;
            yield return new WaitForSeconds(explosiveTime);
            Destroy(gameObject);
        }
       


    }
}