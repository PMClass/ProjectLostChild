using TarodevController;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    private PlayerController _plObject;
    private GameObject _coObject;

    private void Awake()
    {
        if (!TryGetComponent(out _plObject)) Debug.LogWarning("Oops, I cannot find the PlayerController! Where is it?!");
        else
        {
            if (_plObject.CompanionPrefab != null)
            {
                _coObject = Instantiate(_plObject.CompanionPrefab, null);
                _coObject.name = ("Companion");
                Debug.Log("Companion object created!");
            } else Debug.LogWarning("The Companion Prefab inspector value has not been set!");
        }
        
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
