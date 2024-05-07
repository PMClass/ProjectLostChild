using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    PlayerConditions playerConditions;
    public Image healthBar;

    
    
    private void Awake()
    {

        playerConditions = GameObject.FindObjectOfType<PlayerConditions>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerConditions.CurrentHealth / playerConditions.PlayerHealth, 0f, 1f);
    }
}
