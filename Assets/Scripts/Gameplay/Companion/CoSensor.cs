using UnityEngine;

public class CoSensor : MonoBehaviour
{
    [SerializeField] private GameObject InteractableTouched;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided != null)
        {
            if (collided.CompareTag("Interactable"))
            {
                Debug.Log("oh hi there");
                InteractableTouched = collided;
            }
        }

        Debug.Log("I'm touching something");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided != null)
        {
            if (collided.CompareTag("Interactable") && collided == InteractableTouched)
            {
                Debug.Log("oh okay bye");
                InteractableTouched = null;
            }
        }
    }
}
