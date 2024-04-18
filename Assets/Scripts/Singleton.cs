using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    // non-working singleton (does not create itself when Instance is referenced)
    /*
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance != null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }

    }
    */

    // Singleton template
    // https://forum.unity.com/threads/creating-a-proper-game-manager.378606/#post-2480596

    private static volatile T _instance = null;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject go = new GameObject();
                        _instance = go.AddComponent<T>();
                        go.name = typeof(T).ToString();
                        DontDestroyOnLoad(go);
                    }
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

