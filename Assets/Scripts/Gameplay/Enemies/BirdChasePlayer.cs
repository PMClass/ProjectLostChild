using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class BirdChasePlayer : MonoBehaviour
{

    public BirdScript[] birdArray;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(BirdScript enemy in birdArray)
            {
                enemy.isChasable = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(BirdScript enemy in birdArray)
            {
                enemy.isChasable = false;
            }
        }
    }
}
