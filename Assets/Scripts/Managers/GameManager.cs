using UnityEngine.SceneManagement;
using UnityEngine;
using Eflatun.SceneReference;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public enum GMState
    {
        MENU,
        PAUSE,
        GAME,
        LOAD
    }

    #region References
    private GMState currentState = GMState.LOAD;
    private PauseAction pauseCtrl;

    [SerializeField] private SceneReference MenuScene, GameScene;
    [SerializeField] private GameObject UIPausePrefab;
    [SerializeField] private GameObject PlayerPrefab;
    
    #endregion

    #region Interface
    [SerializeField] public float RespawnDelay = 3.0f;
    #endregion

    #region Initial Setup & Loop
    override public void Awake()
    {
        // still call the base class (Singleton) awake function--before running anything in the override.
        base.Awake();
        pauseCtrl = new PauseAction();
    }

    void Start()
    {
        StartMenu();
    }
    
    void Update()
    {
        // these update functions run during gameplay or pause
        if (currentState is GMState.GAME or GMState.PAUSE)
        {
            CalculatePausePressed();
            if (currentState is GMState.GAME)
            {
                CalculatePlayer();
            }
        }
    }
    #endregion

    #region Scene changes
    public void StartMenu()
    {
        // disable pause control
        pauseCtrl.Disable();
        // this function will always load the main menu and set state to MENU
        SceneManager.LoadScene(MenuScene.BuildIndex);
        currentState = GMState.MENU;
    }

    public void StartGame()
    {
        // this function may be called to load the gameplay scene if the current state is not GAME or PAUSE
        if (currentState != GMState.GAME && currentState != GMState.PAUSE)
        {
            StartCoroutine(SetupGameScene());
        }
    }
    #endregion

    #region Utilities
    // Gameplay Setup Function
    IEnumerator SetupGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameScene.Name, LoadSceneMode.Single);
        while (!asyncLoad.isDone) yield return null;
        Debug.Log("Game Scene loaded.");

        currentState = GMState.GAME;
        SetupPauseMenu();
        SetupPlayer();
    }

    #region Pause Menu Functions
    void SetupPauseMenu()
    {
        if (UIPausePrefab != null)
        {
            _pauseUI = Instantiate(UIPausePrefab);

            if (_pauseUI.TryGetComponent<Canvas>(out _pauseCanvas))
            {
                Debug.Log("Pause menu loaded, enabling pause action.");
                pauseCtrl.Enable();
            }
            else Debug.LogWarning("No Canvas in Pause UI!");
        }
        else Debug.LogWarning("No Pause UI Prefab!");
    }

    GameObject _pauseUI;
    Canvas _pauseCanvas;
    public void TogglePause()
    {
        // this function will only run if the current state is GAME or PAUSE
        if (currentState == GMState.PAUSE)
        {
            Debug.Log("Unpausing the game!");
            currentState = GMState.GAME;
            _pauseCanvas.enabled = false;
        }
        else if (currentState == GMState.GAME)
        {
            Debug.Log("Pausing the game!");
            currentState = GMState.PAUSE;
            _pauseCanvas.enabled = true;
        }
        else Debug.Log("Pause now? Um, no. Currently " + currentState.ToString());
    }

    void CalculatePausePressed()
    {
        if (pauseCtrl.Pause.PauseMenu.triggered)
        {
            Debug.Log("Pause button pressed!");
            TogglePause();
        }
    }
    #endregion

    #region Player Management Functions
    GameObject _currentPlayer;
    PlayerConditions _currentConditions;

    void SetupPlayer()
    {
        if (PlayerPrefab != null)
        {
            Transform _spawnTransform = FindSpawnPoint();
            if (_spawnTransform != null)
            {
                _currentPlayer = Instantiate(PlayerPrefab, _spawnTransform); //instantiate with transform
                if (_currentPlayer.TryGetComponent<PlayerConditions>(out _currentConditions))
                {
                    _currentConditions.PlayerSetCheckpoint(_spawnTransform);
                }
                else Debug.LogWarning("No PlayerConditions in Player!");
            }
            else Debug.LogWarning("No checkpoints!");
            
        }
        else Debug.LogWarning("No Player Prefab!");
    }

    Transform FindSpawnPoint()
    {
        Transform _priorityTransform = null;
        int highestPriority = -1;

        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (var checkpoint in checkpoints)
        {
            if (checkpoint.TryGetComponent<CheckpointData>(out var data))
            {
                int _currentPriority = data.GetPriority();
                if (_currentPriority > highestPriority)
                {
                    highestPriority = _currentPriority;
                    _priorityTransform = checkpoint.transform;
                }
            }
        }
        
        return _priorityTransform;
    }

    bool respawnStarted = false;
    void CalculatePlayer()
    {
        if (_currentConditions != null)
        {
            if (_currentConditions.GetPlayerDead() && !respawnStarted)
            {
                StartCoroutine(SpawnTimer());
            }
        }
    }

    IEnumerator SpawnTimer()
    {
        Debug.Log("Begin respawn timer");
        respawnStarted = true;
        yield return new WaitForSeconds(RespawnDelay);
        _currentConditions.PlayerSpawn();
        respawnStarted = false;
    }
    #endregion

    #endregion
}
