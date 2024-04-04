using TarodevController;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    private PlayerController _plObject;
    private GameObject _coObject;
    private Rigidbody2D _coRigid;
    private CoSensor _coSense;

    [SerializeField] private GameObject InteractableTouched;

    private void Awake()
    {
        if (!TryGetComponent(out _plObject))
            Debug.LogWarning("No PlayerController?!");
        else
        {
            if (_plObject.CompanionPrefab != null)
            {
                _coObject = Instantiate(_plObject.CompanionPrefab, null);
                _coObject.name = ("Companion");

                if (!_coObject.TryGetComponent(out _coRigid))
                    Debug.LogWarning("No Rigidbody2D!");

                if (!_coObject.TryGetComponent(out _coSense))
                    Debug.LogWarning("No CoSensor!");

                Debug.Log("Companion object created!");
            } else Debug.LogWarning("No Companion Prefab!");
        }
        
    }

    public void MoveCompanion(Vector2 vel, bool noInput)
    {
        //Vector2 limit = new(10, 10);
        //Vector2 _vel = vel / limit;

        if (!noInput) _coRigid.AddForce(vel, ForceMode2D.Impulse);
        else _coRigid.velocity = new(0,0);
    }
}
