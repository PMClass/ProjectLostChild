using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderArea : MonoBehaviour
{
    public Transform target;
    public float xOffset = 5;
    public Camera mainCamera = Camera.main;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 offset = new Vector2(target.position.x + xOffset, transform.position.y);
        Vector3 newPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0f);
        transform.position = newPos;
    }
}
