using System.Collections;
using TarodevController;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    private PlayerController _playerCtrl;

    // --- Serialized Private Variables
    // PlayerHurt is used by PlayerController and controls jump disabling
    // CanRecover is used by this component only and controls hurt debounce
    // PlayerDead is used by PlayerController and controls player disabling
    [field: SerializeField] private bool PlayerHurt { get; set; } = false;
    [field: SerializeField] private bool CanRecover { get; set; }
    [field: SerializeField] private bool PlayerDead { get; set; }
    // Numerical Health Value
    [field: SerializeField] private float CurrentHealth { get; set; }
    
    // --- Inspector Variables
    // RecoverDelay controls
    [field: SerializeField] public float RecoverDelay { get; private set; } = 2f;
    [field: SerializeField] public float HeightHurtMin = 4f;
    [field: SerializeField] public float HeightDeathMin = 6f;
    // Numerical Health Value
    [field: SerializeField] public bool UseNumericHealth { get; private set; } = true;
    [field: SerializeField] public float PlayerHealth { get; private set; } = 3f;

    private void Awake()
    {
        if (!TryGetComponent(out _playerCtrl))
        {
            Debug.LogWarning("There is no PlayerController, PlayerConditions will not function!");
        }
    }

    void Start()
    {
        PlayerSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // --- Game Functions
    public void PlayerDie()
    {
        Debug.Log("ow, i am dead");
        PlayerDead = true;
        PlayerHurt = true;
        if (UseNumericHealth) CurrentHealth = 0f;
    }
    public void PlayerSpawn()
    {
        CanRecover = true;
        PlayerDead = false;

        if (UseNumericHealth) CurrentHealth = PlayerHealth;
    }

    // PlayerTryRecover is called by the PlayerController to see if jumping is okay
    public void PlayerTryRecover()
    {
        if (PlayerHurt && !PlayerDead)
        {
            if (CanRecover)
            {
                PlayerHurt = false;
                CanRecover = true;
            }
        }
    }

    // give me the pain
    [ContextMenu("Give Player Hurt")]
    public void PlayerGiveHurt()
    {
        // Do not run if player is dead
        if (!PlayerDead)
        {
            if (UseNumericHealth) // new hurt system
            {
                // Only give hurt if recovery is possible
                if (CanRecover)
                {
                    CurrentHealth -= 1f;

                    Debug.Log("I have " + CurrentHealth + " health left!");

                    PlayerHurt = true;
                    CanRecover = false;

                    // If player has at least 1 health, this function call starts the recovery timer
                    if (CurrentHealth > 0)
                    {
                        _playerCtrl.HurtKnockback();

                        // Begin RecoverTimer

                        StartCoroutine(RecoverTimer());
                    }
                    // Otherwise, this function call is fatal
                    else PlayerDie();
                }
            }
            else // legacy hurt system
            {
                // If player is hurt and they can recover, this function call is fatal
                if (PlayerHurt && CanRecover)
                {
                    PlayerDie();
                }
                // If player is not hurt, this function call starts the recovery timer
                else if (!PlayerHurt)
                {
                    PlayerHurt = true;
                    CanRecover = false;

                    _playerCtrl.HurtKnockback();

                    // Begin RecoverTimer

                    StartCoroutine(RecoverTimer());
                }
            }
        }
    }

    public void PlayerCheckFall()
    {
        if (!PlayerDead)
        {
            float _height = _playerCtrl.FallHeight;
            if (_height >= HeightDeathMin) { PlayerDie(); }
            else if (_height >= HeightHurtMin) { PlayerGiveHurt(); }
        }
    }

    // --- Accessors

    public bool GetPlayerHurt()
    {
        return PlayerHurt;
    }

    public bool GetPlayerDead()
    {
        return PlayerDead;
    }

    // -- Coroutine IEnumerators

    IEnumerator RecoverTimer()
    {
        Debug.Log("Timer Started");
        yield return new WaitForSeconds(RecoverDelay);
        CanRecover = true;
        Debug.Log("Timer Ended");
    }
}
