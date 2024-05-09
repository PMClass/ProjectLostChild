using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    string oldLocation = null;
    string currentLocation = null;
    void Update()
    {
        if (gameManager.CurrentState == GameManager.GMState.GAME)
        {
            if (active.Count > 0)
            {
                currentLocation = gameManager.GetCurrentLocation();
                if (currentLocation != oldLocation)
                {
                    Debug.Log("Placing new levels");
                    PlaceCluster(GetIndexFromName(currentLocation));
                    oldLocation = currentLocation;
                }
            }
        }
    }

    private int GetIndexFromName(string name)
    {
        string _filtered = Regex.Match(name, @"\d+").Value;
        try
        {
            int _number = Int32.Parse(_filtered);
            return _number;
        }
        catch
        {
            return -1;
        }
    }

    public void ClearActive()
    {
        active.Clear();
    }

    public void PlaceCluster(int index)
    {
        GameObject centreLevel = GetActive(index);
        if (centreLevel == null)
        {
            centreLevel = PlaceLevel(index, -1, LoadPointDir.NORTH, true);
            active.Add(centreLevel);
        }

        LoadPoint[] points = centreLevel.GetComponentsInChildren<LoadPoint>();
        if (points.Length > 0)
        {
            foreach (LoadPoint point in points)
            {
                LoadPointDir opposite = LoadPoint.GetOpposite(point.Direction);
                GameObject _newLevel = PlaceLevel(point.LevelIndex, index, opposite, true);
                if (_newLevel != null)
                    active.Add(_newLevel);
            }
        }
    }

    private GameObject PlaceLevel(int levelToPlace, int levelToConnect, LoadPointDir dir, bool relocate)
    {
        GameObject _toPlace = GetActive(levelToPlace);
        if (_toPlace == null)
        {
            GameObject _load = GetLoaded(levelToPlace);
            if (_load == null) return null;
            
            _toPlace = Instantiate(_load);
            _toPlace.name = _load.name;
        }

        if (levelToConnect == -1 || active.Count == 0)
        {
            // This is the first level in the world, so place it at origin
            _toPlace.transform.position = Vector2.zero;
        }
        else
        {
            // Get active level for connect
            GameObject _toConnect = GetActive(levelToConnect);
            if (_toConnect == null) return _toPlace;

            // Get opposite direction of parameter dir
            LoadPointDir _opposite = LoadPoint.GetOpposite(dir);

            // Get load point of place level
            LoadPoint placePoint = GetLoadPoint(_toPlace, dir);
            // Get load point of connect level
            LoadPoint connectPoint = GetLoadPoint(_toConnect, _opposite);

            if (placePoint != null && connectPoint != null)
            {
                Transform _destination = connectPoint.gameObject.transform;
                Transform _offset = placePoint.gameObject.transform;

                Vector3 _finalPos = _destination.position - _offset.localPosition;

                _toPlace.transform.position = _finalPos;
            }

            // find opposite LoadPoint transform of levelToPlace
            // find LoadPoint transform dir of levelToConnect
        }

        return _toPlace;
    }

    private LoadPoint GetLoadPoint(GameObject root, LoadPointDir dir)
    {
        LoadPoint[] points = root.GetComponentsInChildren<LoadPoint>();
        if (points.Length == 0) return null;
        foreach (LoadPoint point in points)
        {
            if (point.Direction == dir) return point;
        }
        return null;
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
