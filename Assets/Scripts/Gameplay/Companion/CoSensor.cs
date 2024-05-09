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

    public GameObject GetInteractable()
    {
        return InteractableTouched;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (!collision.isTrigger && collided != null && !collision.CompareTag("LoadingArea"))
        {
            if (collided.CompareTag("Interactable"))
            {
                InteractableTouched = collided;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided != null)
        {
            if (collided.CompareTag("Interactable") && collided == InteractableTouched)
            {
                InteractableTouched = null;
            }
        }
    }
}
