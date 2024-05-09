using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class BoarTrigger : MonoBehaviour
{

   
    [SerializeField] private float xForce;
    [SerializeField] private float yForce;

    public PlayerController playerController;
    public Transform playerTransform;
    public PlayerConditions playerConditions;
    public GameObject playerChar;

    public GameManager gm;

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
        playerController = playerChar.GetComponent<PlayerController>();
        playerTransform = playerChar.transform;
        playerConditions = playerChar.GetComponent<PlayerConditions>();

        enabled = true;
    }

    private void FixedUpdate()
    {
        
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            if (playerTransform.position.x < this.transform.position.x)
            {
                playerController.AddFrameForce(new(-xForce, yForce), true);
            }

            if (playerTransform.position.x > this.transform.position.x)
            {
                playerController.AddFrameForce(new(xForce, yForce), true);
            }

            playerConditions.PlayerGiveHurt();

        }
    }
}
