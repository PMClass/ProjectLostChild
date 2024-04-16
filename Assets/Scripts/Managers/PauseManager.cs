using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    PauseAction action;
    public bool isPaused;
    public GameObject pauseUI;


    private void Awake()
    {
        action = new PauseAction();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {
        pauseUI.SetActive(false);
        action.Pause.PauseMenu.performed += _ => DeterminePause();
    }

    public void DeterminePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
    }


}
