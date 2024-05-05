using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    GameManager gameManager;

    GameObject playerPrefab;
    GameObject coPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Virtual Camera Functions
    CinemachineVirtualCamera virtualCamera;
    GameObject cameraObj;
    public void LoadVirtualCamera()
    {
        
    }

    public void UnloadVirtualCamera()
    {

    }
    #endregion
}
