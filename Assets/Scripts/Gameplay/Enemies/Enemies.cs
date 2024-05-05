using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;



public class Enemy<T> where T : Enemy
{
    public GameObject GameObject;
    public T scriptComponent;

    public Enemy(string name)
    {
        GameObject = new GameObject(name);
        scriptComponent = GameObject.AddComponent<T>(); 
    }
}

public abstract class Enemy : MonoBehaviour
{

    public PlayerController playerCTRL;
    public bool canHit;

    private void Awake()
    {
        canHit = true;
    }

    private void Update()
    {
        
    }

    public virtual void HitPlayer(int xRange, int yRange, float hitDelay)
    {
        if (canHit)
        {
            StartCoroutine(AttackTime(xRange, yRange, hitDelay));
        }
        else
        {
            return;
        }
    }


    IEnumerator AttackTime(int x, int y, float hitDelay)
    {
        playerCTRL.AddFrameForce(new(x, y), true);
        yield return new WaitForSeconds(hitDelay);
        canHit = false;
        yield return new WaitForSeconds(hitDelay);
        canHit = true;
    }

    /* public virtual void Initialize(int speed, int direction, Vector3 position)
     {
         Speed = speed;
         Direction = direction;
         transform.position = position;
     }
    */

}
