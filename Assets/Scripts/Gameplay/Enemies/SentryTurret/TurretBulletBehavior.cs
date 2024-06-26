using System;
using System.Collections;
using TarodevController;
using UnityEngine;

public class TurretBulletBehavior : MonoBehaviour
{
    PlayerController playerController;
    PlayerConditions playerConditions;
    public GameManager gm;
    public GameObject playerChar;

    public float xForce = 1, yForce = 1;
    private void Awake()
    {
        gm = GameManager.Instance;
        
    }
    // Start is called before the first frame update
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

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Companion")
        {
            
        }

        if(other.gameObject.tag == "Player")
        {
            playerController.AddFrameForce(new(xForce, yForce), true);
            playerConditions.PlayerGiveHurt();
        }

        

        Destroy(this.gameObject);
    }
}
