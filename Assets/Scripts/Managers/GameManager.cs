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
    [SerializeField] private SceneReference MenuScene, PauseScene, GameScene;
    #endregion

    #region Inspector Variables
    //
    #endregion

    #region Private Variables
    private GMState currentState = GMState.LOAD;
    #endregion

    #region Setup & Loop
    PauseAction pauseCtrl;

    override public void Awake()
    {
        // still call the base class (Singleton) awake function--before running anything in the override.
        base.Awake();
        pauseCtrl = new PauseAction();
    }

    void Start()
    {
        //pauseCtrl.Pause.PauseMenu.performed += _ => TogglePause();
        StartMenu();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (pauseCtrl.Pause.PauseMenu.triggered)
        {
            Debug.Log("Pause button pressed!");
            TogglePause();
        }
    }
    #endregion

    #region Scene changes
    GameObject _pauseUI;
    Canvas _pauseCanvas;

    public void StartMenu()
    {
        // this function will always load the main menu and set state to MENU
        SceneManager.LoadScene(MenuScene.BuildIndex);
        currentState = GMState.MENU;
    }

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
    IEnumerator SetupGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameScene.Name, LoadSceneMode.Single);
        while (!asyncLoad.isDone) yield return null;
        Debug.Log("Game Scene loaded.");

        currentState = GMState.GAME;
        StartCoroutine(SetupPauseScene());
    }
    IEnumerator SetupPauseScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(PauseScene.Name, LoadSceneMode.Additive);
        while (!asyncLoad.isDone) yield return null;
        Debug.Log("Pause UI Scene loaded. Now looking for pause ui...");

        var newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        GameObject[] SceneRoot = newScene.GetRootGameObjects();
        Debug.Log("Okay so... SceneRoot has " + SceneRoot.Length + " objects.");
        foreach (var obj in SceneRoot)
        {
            Debug.Log("we've got " + obj.name);
            if (obj.name == "Canvas-Pause")
            {
                _pauseUI = obj;
                break;
            }
        }

        if (_pauseUI == null)
        {
            Debug.LogWarning("No GameObject named Canvas-Pause!");
        }
        else
        {
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
    }
    #endregion
}
