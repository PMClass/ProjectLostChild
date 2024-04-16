using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractablePlatform : MonoBehaviour, IObjInteractable
{
    #region References
    private Rigidbody2D _rb;
    #endregion

    #region Inspector Values
    public bool CanMoveX = true;
    public bool CanMoveY = true;
    #endregion

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Interact()
    {

    }

    public void Interact(Vector2 data)
    {
        if (CanMoveX) _rb.AddForce(new(data.x*_rb.mass, 0), ForceMode2D.Impulse);
        if (CanMoveY) _rb.AddForce(new(0, data.y*_rb.mass), ForceMode2D.Impulse);
    }
}