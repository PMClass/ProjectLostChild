using System.Collections;
using System.Collections.Generic;
using TarodevController;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class CompanionController : MonoBehaviour
{
    private PlayerInputActions _actions;
    private InputAction _move;

    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;

    //public GameObject companion;
    public GameObject player;
    
 
    public float rotationSpeed = 5.0f;
    [SerializeField] public float speed = 1.0f;
    public Transform myTransform;
    private void Awake()
    {
        _actions = new PlayerInputActions();
        _move = _actions.Player.Move;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => _actions.Enable();

    private void OnDisable() => _actions.Disable();


    
        
    



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

        // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // this.gameObject.GetComponent<Rigidbody2D>().MovePosition(mousePos);

        // FollowPlayer();
        ControlCompanion();
        
        
    }

    private void FollowPlayer()
    {
        Vector3 offset = player.transform.position;
        offset += new Vector3(3, 3, 0);
        transform.position = Vector2.MoveTowards(transform.position, offset, speed);
    }

   private void ControlCompanion()
    {
        Vector2 inputValue = _move.ReadValue<Vector2>().normalized;
        Vector3 input3 = new Vector3(inputValue.x, inputValue.y, 0f);
        transform.position += input3;
        rb.AddForce(inputValue, ForceMode2D.Force);
    }


}
