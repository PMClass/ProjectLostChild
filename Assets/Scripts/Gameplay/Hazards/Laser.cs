using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public GameManager gm;
    public GameObject playerChar;
    public PlayerConditions playerConditions;
    public PlayerController playerController;

    public float xForce = 3;
    public float yForce = 1;
    private void Awake()
    {
        gm = GameManager.Instance;
    }

    IEnumerator Start()
    {
        enabled = false;

        while(gm.CurrentState != GameManager.GMState.GAME)
        {
            yield return null;
        }

        playerChar = gm.CurrentPlayer;
        playerConditions = playerChar.GetComponent<PlayerConditions>();
        playerController = playerChar.GetComponent<PlayerController>();

        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerConditions.PlayerGiveHurt();
            playerController.AddFrameForce(new(xForce, yForce), true);
        }
    }
}
