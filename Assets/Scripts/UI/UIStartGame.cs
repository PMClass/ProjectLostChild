using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIStartGame : MonoBehaviour
{
    [SerializeField] Button _startGame;

    void Start()
    {
        _startGame.onClick.AddListener(StartNewGame);
        
    }

    public void StartNewGame()
    {
        ScenesManager.instance.LoadNewGame();
    }



}
