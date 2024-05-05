using UnityEngine;
using UnityEngine.UI;

public class UIStartGame : MonoBehaviour
{
    
    private void Update()
    {
        if(Input.anyKeyDown) StartNewGame();
    }

    public void StartNewGame()
    {       
        GameManager.Instance.StartGame();
    }


}
