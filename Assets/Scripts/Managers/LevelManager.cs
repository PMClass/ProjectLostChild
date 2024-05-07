using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static LoadPoint;

public class LevelManager : MonoBehaviour
{
    #region References
    GameManager gameManager;

    private List<GameObject> loaded = new();
    private List<GameObject> active = new();
    #endregion

    #region Interface
    [SerializeField] private int MaxLevels = 10;
    [SerializeField] public string LevelPrefix { get; private set; } = "Level";

    public void PreloadLevels(int offset)
    {
        for (int i = offset; i < MaxLevels; i++)
        {
            GameObject preloaded = Resources.Load<GameObject>("Levels/" + LevelPrefix + i);
            if (preloaded != null)
            {
                loaded.Add(preloaded);
            }
        }
    }
    #endregion

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    string oldLocation = null;
    string currentLocation = null;
    void Update()
    {
        if (gameManager.CurrentState == GameManager.GMState.GAME)
        {
            currentLocation = gameManager.GetCurrentLocation();
            if (currentLocation != oldLocation)
            {
                Debug.Log("Placing new levels");
                PlaceCluster(currentLocation);
                oldLocation = currentLocation;
            }
        }
    }

    private void UpdateActive(List<GameObject>toUpdate)
    {
        foreach (GameObject g in toUpdate)
        {
            if (!active.Contains(g))
            {
                active.Add(g);
            }
        }
    }

    public void PlaceCluster(string location)
    {
        GameObject currentLevel;
        List<GameObject> newActive = new();

        /*
        GameObject existing = GetExistingLevel(location);
        if (existing != null)
            currentLevel = existing;
        else
            currentLevel = PlaceLevel(location, Vector2.zero);
        
        if (currentLevel != null)
        {
            newActive.Add(currentLevel);
            LoadPoint[] loadPoints = currentLevel.GetComponentsInChildren<LoadPoint>();
            if (loadPoints.Length > 0)
            {
                foreach (LoadPoint loadPoint in loadPoints)
                {
                    if (loadPoint.LevelName.Length > 0)
                    {
                        GameObject connected = PlaceLevel(loadPoint.LevelName, loadPoint.gameObject.transform.position);
                        newActive.Add(connected);
                    }
                    else Debug.Log("Blank load point");
                }

                UpdateActive(newActive);
            }
        }
        */
    }

    private GameObject PlaceLevel(int levelToPlace, int levelToConnect, LoadPointDir dir, bool relocate)
    {
        GameObject toPlace = GetActive(levelToPlace);
        if (toPlace == null)
        {
            GameObject _load = GetLoaded(levelToPlace);
            if (_load == null) return null;
            
            toPlace = Instantiate(_load);
        }

        if (levelToConnect == -1 || active.Count == 0)
        {
            toPlace.transform.position = Vector2.zero;
        }
        else
        {
            GameObject toConnect = GetActive(levelToConnect);
            if (toConnect == null) return null;

            // find opposite LoadPoint transform of levelToPlace
            // find LoadPoint transform dir of levelToConnect
        }

        return toPlace;
    }

    private GameObject GetLoaded(int level)
    {
        if (loaded != null & loaded.Count > 0)
        {
            foreach (GameObject l in loaded)
            {
                string toMatch = LevelPrefix + level;
                if (l.name == toMatch && l != null)
                    return l;
            }
        }
        return null;
    }

    private GameObject GetActive(int level)
    {
        if (active != null & active.Count > 0)
        {
            foreach (GameObject l in active)
            {
                string toMatch = LevelPrefix + level;
                if (l.name == toMatch && l != null)
                    return l;
            }
        }
        return null;
    }
}
