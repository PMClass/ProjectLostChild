using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class CompanionController : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;

    public GameObject companion;
    public GameObject player;
    
 
    public float rotationSpeed = 5.0f;
    [SerializeField] public float speed = 1.0f;
    public Transform myTransform;
    private void Start()
    {
     
       
        //myTransform = player.transform;

       Cursor.lockState = CursorLockMode.Confined;
       
    }

    private void Update()
    {

        // Rotate to player
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(myTransform.transform.position - transform.position), rotationSpeed * Time.deltaTime);

        //transform.position += transform.forward * speed * Time.deltaTime;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        this.gameObject.GetComponent<Rigidbody2D>().MovePosition(mousePos);
       
       
        
    }

    private void FollowPlayer()
    {
        companion.transform.position = Vector2.MoveTowards(companion.transform.position, player.transform.position, speed);
    }

   


}
