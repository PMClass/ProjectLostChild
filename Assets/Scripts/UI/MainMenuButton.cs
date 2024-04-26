using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    public void MainMenu()
    {
        GameManager.Instance.StartMenu();
    }

    public void Resume()
    {
        GameManager.Instance.TogglePause();
    }
   
}
