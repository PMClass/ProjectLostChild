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
   

    private void Awake()
    {
        playerCTRL = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        
    }

    public virtual void HitPlayer(int xRange, int yRange)
    {
         
        playerCTRL.AddFrameForce(new(xRange, yRange), true);
     
    }


   

    /* public virtual void Initialize(int speed, int direction, Vector3 position)
     {
         Speed = speed;
         Direction = direction;
         transform.position = position;
     }
    */

}
