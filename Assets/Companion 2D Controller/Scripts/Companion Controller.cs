using System.Collections;
using System.Collections.Generic;
using TarodevController;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class CompanionController : MonoBehaviour
{

    private PlayerInputActions _actions, _switch;
    private InputAction _move;

    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;

    public GameObject player;
    public Transform followPoint;
    [SerializeField] private float speed = 1.0f;
    public float hoverSpeed = 2.0f;
    [SerializeField] private bool isControlled;
    private void Awake()
    {
        isControlled = false;
        _actions = new PlayerInputActions();
        _move = _actions.Player.Move;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _actions.Enable();
        _switch.Enable();
    }  

    private void OnDisable()
    {
        _actions.Disable();
        _switch.Disable();
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

        ControlCompanion();
   
        
    }

    private void FollowPlayer()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, followPoint.position, hoverSpeed);
    }

   private void ControlCompanion()
    {
        Vector2 inputValue = _move.ReadValue<Vector2>();
        inputValue.Normalize();
        rb.velocity = new Vector2(inputValue.x * speed, inputValue.y * speed);
    }


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
