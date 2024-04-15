using TarodevController;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    #region References
    private PlayerController _plObject;
    private GameObject _coObject;
    private Rigidbody2D _coRigid;
    private CoSensor _coSense;
    #endregion

    #region Private Members
    private bool InControl = false;
    private GameObject InteractableObject;
    #endregion

    #region Private Serialized
    //[SerializeField] private GameObject InteractableTouched;
    [SerializeField] private IObjInteractable InteractableControlled;
    #endregion

    #region Setup
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
    #endregion
    
    public void ToggleControlInteractable()
    {
        if (!InControl)
        {
            GameObject InteractableTouched = _coSense.GetInteractable();
            if (InteractableTouched != null)
            {
                var tempMonoArray = InteractableTouched.GetComponents<MonoBehaviour>();

                foreach (var monoBehaviour in tempMonoArray)
                {
                    var tempInteractable = monoBehaviour as IObjInteractable;

                    if (monoBehaviour is IObjInteractable)
                    {
                        InteractableControlled = tempInteractable;
                        InControl = true;
                        InteractableObject = InteractableTouched;
                        break;
                    }
                }
            }
        }
        else
        {
            InteractableControlled = null;
            InControl = false;
        }
    }

    public bool HasControllableObj() => InteractableControlled != null;

    public void MoveCompanion(Vector2 vel, bool noInput)
    {
        if (InControl && HasControllableObj())
        {
            InteractableControlled.Interact(vel);
            _coRigid.MovePosition(InteractableObject.transform.position);
        }
        else
        {
            if (!noInput) _coRigid.AddForce(vel, ForceMode2D.Impulse);
            else
            {
                var InteractableObj = _coSense.GetInteractable();
                if (InteractableObj != null)
                {
                    _coRigid.MovePosition(InteractableObj.transform.position);
                }
                _coRigid.velocity = new(0, 0);

            }
        }
    }
}