using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    GameManager gm;
    GameObject playerChar;

    Transform target;

    private void Awake()
    {
        gm = GameManager.Instance;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        enabled = false;
        Debug.LogWarning("Wait for second!!");
        while (gm.CurrentState != GameManager.GMState.GAME)
        {
            yield return null;
        }
        Debug.LogWarning("Im done!");

        playerChar = gm.CurrentPlayer;
        target = playerChar.transform;

        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarning("look, the player is at" + target.position);
    }
}
