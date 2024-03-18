using TarodevController;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class CompanionController : MonoBehaviour
{
    public Rigidbody2D rb;
    public  float moveSpeed = 2.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
       
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if(collision.gameObject.layer == LayerMask.NameToLayer("MoveablePlatform"))
        {
            Debug.Log("The collision is " + collision.gameObject);
        }
        else
        {
            Debug.Log("Nothing is changing");
        }*/
    } 

}
