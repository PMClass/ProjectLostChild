using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class GasStream : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gm;

    public PlayerConditions playerConditions;
    public PlayerController playerController;
    public GameObject playerChar;

    public float xForce = 1, yForce = 1;
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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerConditions.PlayerGiveHurt();
            playerController.AddFrameForce(new(xForce, yForce), true);
        }
    }
}
