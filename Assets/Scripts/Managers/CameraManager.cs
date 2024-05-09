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

    #region Interface
    [field: SerializeField] public float PlayerWeight { get; private set; } = 1f;
    [field: SerializeField] public float CompanionWeight { get; private set; } = 1f;

    [field: SerializeField] public float PlayerRadius { get; private set; } = 3.0f;
    [field: SerializeField] public float CompanionRadius { get; private set; } = 3.0f;
    #endregion

    private void Awake()
    {
        
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        cam = Camera.main;
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
    CinemachineFramingTransposer transposer;
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

            if (transposer == null) { virtualCamera.AddCinemachineComponent<CinemachineFramingTransposer>(); }

            group.AddMember(playerPrefab.transform, PlayerWeight, PlayerRadius);
            group.AddMember(coPrefab.transform, CompanionWeight, CompanionRadius);

            virtualCamera.m_Follow = group.transform;
            virtualCamera.m_LookAt = group.transform;

        }
    }

    public void UnloadVirtualCamera()
    {
        if (group != null)
        {
            group.RemoveMember(playerPrefab.transform);
            group.RemoveMember(coPrefab.transform);
        }
        //composer = null;
        //group = null;
        //transposer = null;
    }
    #endregion
}
