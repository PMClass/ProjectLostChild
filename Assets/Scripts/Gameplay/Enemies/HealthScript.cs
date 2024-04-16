using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    public int health;
    public GameObject gm;

   
    public int Health
    {
        get => health;

        set
        {
            var isDamage = value < health;
            health = Mathf.Clamp(value, 0, maxHealth);

            if (isDamage)
            {
                Damaged?.Invoke(health);
                Debug.Log("Damaged Invoked");
            }         

            if(health <= 0)
            {
                Died?.Invoke();
                
            }
        }
    }


    public UnityEvent<int> Damaged;
    public UnityEvent Died;
    


    private void Awake()
    {
        health = maxHealth;
        
    }

    




    public void Damage(int amount)
    {
        health -= amount;       
        Damaged?.Invoke(health);
    } 

    public void Dead() => Died?.Invoke();

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void NotActive()
    {
        gm.SetActive(false);
    }

   
}
