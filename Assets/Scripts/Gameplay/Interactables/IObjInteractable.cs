using UnityEngine;

public interface IObjInteractable
{
    bool Controllable();
    void Interact();
    void Interact(Vector2 data);
}
