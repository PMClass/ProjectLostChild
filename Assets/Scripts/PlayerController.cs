using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerInputActions pInput;

    public float moveSpeed = 3;
    public float jumpForce = 9;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool isGrounded;
    private Vector2 moveInput;
    private Rigidbody2D rb;


    // Start is called before the first frame update

    private void Awake()
    {
        pInput = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        pInput.Enable();
    }

    private void OnDisable()
    {
        pInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
        if(isGrounded)
            moveInput = pInput.Player.Move.ReadValue<Vector2>();

        if (pInput.Player.Jump.triggered && isGrounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (!pInput.Player.Jump.IsPressed() && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }
}
