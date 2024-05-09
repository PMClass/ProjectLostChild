using System;
using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float maxSize;
    public float growFactor;
    public float waitTime;
    public bool platformTriggered;


    void Awake()
    {
        platformTriggered = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            if (platformTriggered == false)
            {
                platformTriggered = true;
                StartCoroutine(Snap());
            }
        }
        
    }

    IEnumerator Snap()
    {
        float timer = 0;

        while (platformTriggered == true)
        {
            while (maxSize > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);

            timer = 0;
            while (1 < transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }

            platformTriggered = false;
        }
    }
}