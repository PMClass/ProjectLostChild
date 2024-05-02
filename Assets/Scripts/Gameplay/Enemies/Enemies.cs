using System.Collections;
using System.Collections.Generic;
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
    public int Speed;
    public int Direction;

    public virtual void Initialize(int speed, int direction, Vector3 position)
    {
        Speed = speed;
        Direction = direction;
        transform.position = position;
    }


}
