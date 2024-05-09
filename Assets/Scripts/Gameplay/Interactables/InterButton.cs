using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]

public class InteractableButton : MonoBehaviour, IObjInteractable
{
    AudioSource button;
    public AudioClip buttonPress;
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

    public void Interact() {
        button.PlayOneShot(buttonPress);
        OnInteractEvent.Invoke(); }
    public void Interact(Vector2 input) { }
}
