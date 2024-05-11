using UnityEngine;
using TarodevController;

public class InteractablePlatform : PlatformBase, IObjInteractable
{
    #region References

    #endregion

    #region Inspector Values
    public bool CanMoveX = true;
    public bool CanMoveY = true;
    public float MoveSpeed = 5f;
    #endregion

    #region Private Vars
    private Vector2 _startPosition;
    private Vector2 _offset;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        PhysicsSimulator.Instance.RemovePlatform(this);
        _startPosition = Rb.position;
    }

    bool inputDebounce = false;

    protected override Vector2 Evaluate(float delta)
    {
        inputDebounce = false;
        return Vector2.Lerp(transform.position, (Vector2)transform.position + _offset, delta*MoveSpeed);
    }

    public bool Controllable() { return true; }

    public void Interact()
    {

    }

    public void Interact(Vector2 data)
    {
        _offset = Vector2.zero;
        if (!inputDebounce)
        {
            inputDebounce = true;
            if (CanMoveX) _offset += new Vector2(data.x, 0);
            if (CanMoveY) _offset += new Vector2(0, data.y);
        }
    }

    public void EnablePlatform(bool enable)
    {
        if (enable)
            PhysicsSimulator.Instance.AddPlatform(this);
        else
            PhysicsSimulator.Instance.RemovePlatform(this);
    }
}