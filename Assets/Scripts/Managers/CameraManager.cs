using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    GameManager gameManager;

    GameObject playerPrefab;
    GameObject coPrefab;
    Camera cam;
    GameObject cameraObj;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            cameraObj = cam.gameObject;
        }
        else
        {
            Debug.LogWarning("No Camera object!");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Virtual Camera Functions
    CinemachineVirtualCamera virtualCamera;
    CinemachineGroupComposer composer;
    CinemachineTargetGroup group;
    public void LoadVirtualCamera()
    {
        playerPrefab = gameManager.CurrentPlayer;
        coPrefab = gameManager.CurrentCompanion;

        if (playerPrefab != null && coPrefab != null)
        {
            if (virtualCamera == null)
            {
                virtualCamera = cameraObj.AddComponent<CinemachineVirtualCamera>();
            }

            if (composer == null)
            {
                composer = virtualCamera.AddCinemachineComponent<CinemachineGroupComposer>();
            }

            if (group == null)
            {
                group = cameraObj.AddComponent<CinemachineTargetGroup>();
            }

            group.AddMember(playerPrefab.transform, 1, 10);
            group.AddMember(coPrefab.transform, 1, 10);

        }
    }

    public void UnloadVirtualCamera()
    {

    }
    #endregion
}
