using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] scenesToLoad;
    [SerializeField] private SceneField[] scenesToUnload;


    GameObject player;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
   
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            LoadScenes();
            UnloadScenes();
        }
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneManager.sceneCount);
        
    }

    private void LoadScenes()
    {
        
        for(int i = 0; i < scenesToLoad.Length; i++)
        {
            bool isSceneLoaded = false;

           
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);

                if(loadedScene.name == scenesToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }

            }
            // Add scenes if they're not already loaded in
            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);
            }
        }

    }

    private void UnloadScenes()
    {
        for(int i = 0; i < scenesToUnload.Length; i++)
        {
            for(int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);

                if(loadedScene.name == scenesToUnload[i].SceneName)
                {
                  
                    SceneManager.UnloadSceneAsync(scenesToUnload[i]);
                }
            }
        }
    }
}
