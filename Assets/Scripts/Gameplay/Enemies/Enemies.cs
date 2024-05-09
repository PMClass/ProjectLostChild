using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityHFSM;

/*
public class Enemy<T> where T : Enemy
{
    public GameObject GameObject;
    public T scriptComponent;

    public Enemy(string name)
    {
        GameObject = new GameObject(name);
        scriptComponent = GameObject.AddComponent<T>(); 
    }
}
*/

public abstract class Enemy : MonoBehaviour
{
    #region References
    protected GameManager gm;

    protected GameObject playerChar;
    protected Transform playerTransform;
    protected PlayerController playerControl;
    protected PlayerConditions playerConditions;

    protected Rigidbody2D rb;
    #endregion

    #region States
    protected StateMachine sm = new StateMachine();
    #endregion

    #region Setup
    public virtual IEnumerator Start()
    {
        // get gamemanager
        gm = GameManager.Instance;

        // disable first
        enabled = false;

        // wait for game manager
        while (gm.CurrentState != GameManager.GMState.GAME)
        {
            yield return null;
        }

        // get references to player objects
        playerChar = gm.CurrentPlayer;
        playerTransform = playerChar.transform;
        playerControl = gm.CurrentController;

        // define state machine
        DefineStates();
        DefineTransitions();
        sm.Init();

        enabled = true;
    }

    protected abstract void DefineStates();
    protected abstract void DefineTransitions();
    #endregion

    protected virtual void Update()
    {
        sm.OnLogic();
    }

    public virtual void HitPlayer(int xRange, int yRange)
    {
         
        playerControl.AddFrameForce(new(xRange, yRange), true);
     
    }


   

    /* public virtual void Initialize(int speed, int direction, Vector3 position)
     {
         Speed = speed;
         Direction = direction;
         transform.position = position;
     }
    */

}
