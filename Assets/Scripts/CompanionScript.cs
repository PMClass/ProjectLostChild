using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionScript : MonoBehaviour
{
    CompanionInputActions tia;
    
    public Transform followPoint;  // The target transform to chase
    public Transform _player;
    public float moveSpeed = 2f;  // The speed at which the object moves towards the target
    [SerializeField] bool isControlled;
    [SerializeField] Vector2 moveInput;

    private void Awake()
    {
        tia = new CompanionInputActions();
    }


    private void OnEnable()
    {
        tia.Enable();
    }

    private void OnDisable()
    {
        tia.Disable();
    }


    void Update()
    {
      /*  if (tia.Player.Switch.IsPressed()) isControlled = true;
        else isControlled = false;
        
        
        if (!isControlled && followPoint != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = followPoint.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            // Move towards the target at a constant speed
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

        else
        {
            moveInput = tia.Player.Move.ReadValue<Vector2>();
            transform.Translate(new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.deltaTime);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x , _player.position.x - 3, _player.position.x + 3),
                Mathf.Clamp(transform.position.y, _player.position.y - 3, _player.position.y + 3));
        } */
    }
}
