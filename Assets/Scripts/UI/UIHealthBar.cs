using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    PlayerConditions playerConditions;
    public Image healthBar;

    GameManager gm;
    GameObject playerChar;
    private void Awake()
    {
        gm = GameManager.Instance;
    }

    public IEnumerator Start()
    {
        enabled = false;

        while(gm.CurrentState != GameManager.GMState.GAME)
        {
            yield return null;
        }


        playerChar = gm.CurrentPlayer;

        playerConditions = playerChar.GetComponent<PlayerConditions>();
        enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerConditions.CurrentHealth / playerConditions.PlayerHealth, 0f, 1f);
    }
}
