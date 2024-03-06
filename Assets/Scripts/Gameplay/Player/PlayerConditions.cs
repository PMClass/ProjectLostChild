using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    private PlayerController playerController;

    [field: SerializeField] public bool PlayerHurt { get; private set; } = false;
    // CanRecover used by PlayerController to determine if player can jump to exit hurt state
    public bool CanRecover { get; private set; }
    [field: SerializeField] public bool PlayerDead { get; private set; }
    // InvulnerableTime used by RecoverTimer.
    [field: SerializeField] public float RecoverDelay { get; private set; } = 2f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDie()
    {

    }

    public void PlayerSpawn()
    {
        CanRecover = true;
        PlayerDead = false;
    }

    public void PlayerTryRecover()
    {
        Debug.Log("Alright Imma recover");
        if (PlayerHurt && !PlayerDead)
        {
            Debug.Log("Not dead, hurt.");
            if (CanRecover)
            {
                Debug.Log("Jump!");
                PlayerHurt = false;
                CanRecover = true;
            }
            else Debug.Log("I can't do it!!");
        }
        
    }

    // give me the pain
    [ContextMenu("Give Player Hurt")]
    public void PlayerGiveHurt()
    {
        // Only run if player isn't dead.
        if (!PlayerDead)
        {
            // Player cannot die if they are unable to recover
            if (PlayerHurt && CanRecover)
            {
                PlayerDie();
            }
            else
            {
                // Call HurtKnockback
                

                PlayerHurt = true;
                CanRecover = false;

                // Begin RecoverTimer

                StartCoroutine(RecoverTimer());
            }
        }
    }

    IEnumerator RecoverTimer()
    {
        Debug.Log("Timer Started");
        yield return new WaitForSeconds(RecoverDelay);
        CanRecover = true;
        Debug.Log("Timer Ended");
    }
}
