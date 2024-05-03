using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableButton : MonoBehaviour, IObjInteractable
{
    public UnityEvent OnInteractEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Controllable() { return false; }

    public void Interact() { OnInteractEvent.Invoke(); }
    public void Interact(Vector2 input) { }
}
