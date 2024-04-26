using UnityEngine.SceneManagement;
using UnityEngine;
using Eflatun.SceneReference;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField] private SceneReference MenuScene, GameScene;
    [SerializeField] private GameObject UIPausePrefab;
    [SerializeField] private GameObject PlayerPrefab;
    #endregion

    #region Inspector Variables
    //
    #endregion

    #region Private Variables
    private GMState currentState = GMState.LOAD;
    #endregion

    #region Initial Setup & Loop
    PauseAction pauseCtrl;

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
    }

    #region Update Functions
    void CalculatePausePressed()
    {
        if (pauseCtrl.Pause.PauseMenu.triggered)
        {
            Debug.Log("Pause button pressed!");
            TogglePause();
        }
    }
    #endregion

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
            else
            {
                Debug.LogWarning("No Canvas in Pause UI!");
            }
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
    #endregion

    #region Player Management Functions
    GameObject _currentPlayer;

    #endregion

    #endregion
}
