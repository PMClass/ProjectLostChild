using System.Collections;
using System.Collections.Generic;
using TarodevController;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class CompanionController : MonoBehaviour
{

    private PlayerInputActions _actions, tia;
    private InputAction _move;

    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;

    
    public Transform player;
    public Transform followPoint;
    public  float moveSpeed = 2.0f;
    [SerializeField] private bool isControlled;
    public Vector2 moveInput;
    private void Awake()
    {
        isControlled = false;
        _actions = new PlayerInputActions();
        tia = new PlayerInputActions();
        _move = _actions.Player.Move;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _actions.Enable();
        tia.Enable();
    }  

    private void OnDisable()
    {
        _actions.Disable();
        tia.Disable();
    }  

    
    private void Start()
    {
       
    }

    private void Update()
    {

        // Rotate to player
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(myTransform.transform.position - transform.position), rotationSpeed * Time.deltaTime);

        //transform.position += transform.forward * speed * Time.deltaTime;

        // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // this.gameObject.GetComponent<Rigidbody2D>().MovePosition(mousePos);

        /* if (_switch.Player.Switch.IsPressed())
         {
             isControlled = true;
         }
         else
         {
             isControlled = false;
         }

         if (!isControlled)
         {
             FollowPlayer();
         }
         else
         {
             ControlCompanion();
         } */

        if (tia.Player.Switch.IsPressed()) isControlled = true;
        else isControlled = false;

        // While the companion is not controlled
        if(!isControlled && followPoint == null)
        {
            // Calculate the direction towards the target
            Vector3 direction = followPoint.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            // Move towards the target at constant speed
            transform.Translate(direction * moveSpeed * Time.deltaTime); 

        }
        // Companion is controlled
        else
        {
            moveInput = tia.Player.Move.ReadValue<Vector2>();
            transform.Translate(new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.deltaTime);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, player.position.x - 3, player.position.x + 3),
            Mathf.Clamp(transform.position.y, player.position.y - 3, player.position.y + 3));
        }


        // ControlCompanion();
   
        
    }

   /* private void FollowPlayer()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, followPoint.position, hoverSpeed);
    }*/

   /*private void ControlCompanion()
    {
        Vector2 inputValue = _move.ReadValue<Vector2>();
        inputValue.Normalize();
        rb.velocity = new Vector2(inputValue.x * speed, inputValue.y * speed);
    }*/


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("MoveablePlatform"))
        {
            Debug.Log("The collision is " + collision.gameObject);
        }
        else
        {
            Debug.Log("Nothing is changing");
        }
    } 

}
