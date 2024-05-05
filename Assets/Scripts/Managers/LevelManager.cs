using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region References
    GameManager gameManager;
    
    private List<GameObject> Active = new();
    #endregion

    #region Interface
    [SerializeField] private List<GameObject> Levels;
    [SerializeField] private int MaxLevels = 4;
    #endregion

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        Levels = gameManager.Levels;
        if (Levels.Count == 0)
        {
            Debug.Log("oh, there aren't anything to load :(");
            enabled = false;
        }
    }

    string oldLocation = null;
    string currentLocation = null;
    void Update()
    {
        currentLocation = gameManager.GetCurrentLocation();
        if (currentLocation != oldLocation)
        {
            Debug.Log("Loading new levels");
            Load(currentLocation);
            oldLocation = currentLocation;
        }
    }

    private void UpdateActive(List<GameObject>toUpdate)
    {
        foreach (GameObject g in toUpdate)
        {
            if (!Active.Contains(g))
            {
                Active.Add(g);
            }
        }
    }

    public void Load(string location)
    {
        GameObject currentLevel;
        List<GameObject> newActive = new();

        GameObject existing = GetExistingLevel(location);
        if (existing != null)
            currentLevel = existing;
        else
            currentLevel = LoadLevel(location, Vector2.zero);
        
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
                        GameObject connected = LoadLevel(loadPoint.LevelName, loadPoint.gameObject.transform.position);
                        newActive.Add(connected);
                    }
                    else Debug.Log("Blank load point");
                }

                UpdateActive(newActive);
            }
        }
    }

    private GameObject GetExistingLevel(string level)
    {
        if (Active.Count > 0)
        {
            foreach (GameObject obj in Active)
            {
                if (obj.name == level) return obj;
            }
        }
        return null;
    }

    private GameObject LoadLevel(string level, Vector3 pos)
    {
        Levels = gameManager.Levels;

        GameObject existing = GetExistingLevel(level);
        if (existing != null)
        {
            existing.transform.position = pos;
            Debug.Log("Found existing level");
            return existing;
        }

        if (Levels == null)
        {
            Debug.Log("yeah, we're screwed.");
        }
        foreach (GameObject obj in Levels)
        {
            if (obj.name == level)
            {
                string prefab = "Prefabs/Levels/" + obj.name;
                //GameObject newLevel = Instantiate(Resources.Load<GameObject>(prefab), pos, Quaternion.identity);
                GameObject newLevel = Instantiate(obj,pos,Quaternion.identity);
                newLevel.name = level;
                Debug.Log("Found new level");
                return newLevel;
            }
        }
        return null;
    }
}
