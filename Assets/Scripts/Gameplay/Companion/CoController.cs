using TarodevController;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    #region References
    private PlayerController _plObject;
    private PlayerConditions _plConditions;
    private GameObject _coObject;
    [SerializeField] public Rigidbody2D _coRigid { get; private set; }
    private CoSensor _coSense;
    #endregion

    #region Private Members
    private bool InControl = false;
    private GameObject InteractableObject;
    private Animator _coAnim;
    #endregion

    #region Private Serialized
    //[SerializeField] private GameObject InteractableTouched;
    [SerializeField] private IObjInteractable InteractableControlled;
    [SerializeField] private float MaxDistanceFromPlayer = 10f;
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

                if (!_coObject.TryGetComponent(out Rigidbody2D rigid))
                    Debug.LogWarning("No Rigidbody2D!");
                else
                _coRigid = rigid;

                if (!_coObject.TryGetComponent(out _coSense))
                    Debug.LogWarning("No CoSensor!");

                if (!TryGetComponent(out _plConditions))
                    Debug.LogWarning("No PlayerConditions!");

                Debug.Log("Companion object created!");
            } else Debug.LogWarning("No Companion Prefab!");
        }
        
    }

    private GameObject _anchor;
    private Rigidbody2D _anchorRb;
    private DistanceJoint2D _anchorJoint;

    private void Start()
    {
        _anchor = new("Anchor");
        _anchorRb = _anchor.AddComponent<Rigidbody2D>();
        _anchorRb.isKinematic = true;

        _anchorJoint = _anchor.AddComponent<DistanceJoint2D>();
        _anchorJoint.maxDistanceOnly = true;
        _anchorJoint.autoConfigureDistance = false;
        _anchorJoint.distance = MaxDistanceFromPlayer;
        _anchorJoint.connectedBody = _coRigid;
    }
    #endregion

    public void ToggleControlInteractable()
    {
        if (!InControl)
        {
            GameObject InteractableTouched = _coSense.GetInteractable();
            if (InteractableTouched != null && !tooFar)
            {
                var tempMonoArray = InteractableTouched.GetComponents<MonoBehaviour>();

                foreach (var monoBehaviour in tempMonoArray)
                {
                    var tempInteractable = monoBehaviour as IObjInteractable;

                    if (monoBehaviour is IObjInteractable)
                    {
                        InteractableObject = InteractableTouched;

                        // check if this object can be controlled
                        // if not, just call its Interact() function.
                        if (tempInteractable.Controllable())
                        {
                            InteractableControlled = tempInteractable;
                            InControl = true;

                            if (tempInteractable is InteractablePlatform)
                            {
                                InteractablePlatform interactPlatform = tempInteractable as InteractablePlatform;
                                interactPlatform.EnablePlatform(true);
                            }
                        }
                        else tempInteractable.Interact();
                        
                        break;
                    }
                }
            }
        }
        else
        {
            if (InteractableControlled is InteractablePlatform)
            {
                InteractablePlatform interactPlatform = InteractableControlled as InteractablePlatform;
                interactPlatform.EnablePlatform(false);
            }

            InteractableControlled = null;
            InControl = false;
        }
    }

    public GameObject GetCompanionObject() { return _coObject; }
    
    public bool HasControllableObj() => InteractableControlled != null;

    public void MoveCompanion(Vector2 vel, bool noInput)
    {
        if (!_plConditions.GetPlayerDead())
        {
            if (InControl && HasControllableObj())
            {
                _anchorJoint.enabled = false;
                // send vel to Interact function
                InteractableControlled.Interact(vel);
                _coRigid.MovePosition(InteractableObject.transform.position);
            }
            else
            {
                _anchorJoint.enabled = true;
                // add force using vel when there is player control
                if (!noInput)
                {
                    _coRigid.AddForce(vel, ForceMode2D.Impulse);
                    _coAnim = _coObject.GetComponentInChildren<Animator>();

                    if (_coRigid.velocity.x < 0)
                    {

                        _coAnim.SetBool("isMovingLeft", true);
                        _coAnim.SetBool("isMovingRight", false);
                    }

                    else if (_coRigid.velocity.x > 0)
                    {

                        _coAnim.SetBool("isMovingRight", true);
                        _coAnim.SetBool("isMovingLeft", false);
                    }

                }
                else
                {
                    // if the CoSensor is touching an interactable, reposition to its center
                    var InteractableObj = _coSense.GetInteractable();
                    if (InteractableObj != null && !tooFar)
                    {
                        _coRigid.MovePosition(InteractableObj.transform.position);
                    }
                    // set velocity to zero
                    _coRigid.velocity = Vector2.zero;

                }
            }
        }
        else _anchorJoint.enabled = false;
    }

    private void FixedUpdate()
    {
        DistanceCheck();
        if (tooFar && HasControllableObj())
        {
            ToggleControlInteractable();
        }
        //Debug.Log(_coObject.transform.position.x);
    }

    bool tooFar;
    void DistanceCheck()
    {
        _anchor.transform.position = transform.position;
        
        float _cDistance = Vector2.Distance(gameObject.transform.position, _coObject.transform.position);
        float excess = _cDistance - MaxDistanceFromPlayer * 1.2f;
        tooFar = excess > 0.0f;
    }
}
