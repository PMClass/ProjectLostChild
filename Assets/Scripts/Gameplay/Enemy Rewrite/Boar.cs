using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

public class Boar : Enemy
{
    #region Interface
    public float DistanceToPursue = 3f;
    public float DistanceToStop = 4f;

    public float RoamSpeed = 2f;
    public float PursueSpeed = 3f;
    #endregion

    private float distanceFromPlayer = 100f;

    #region Setup
    public override IEnumerator Start()
    {
        base.Start();

        if (TryGetComponent<Rigidbody2D>(out rb))
        {

        }

        yield return null;
    }

    protected override void DefineStates()
    {
        sm.AddState("Roam");
        sm.AddState("Pursue");
        sm.AddState("Attack");
        sm.AddState("Rest");

        sm.SetStartState("Roam");
    }

    protected override void DefineTransitions()
    {
        sm.AddTwoWayTransition("Roam", "Pursue", transition => distanceFromPlayer <= DistanceToPursue);
        sm.AddTwoWayTransition("Pursue", "Roam", transition => distanceFromPlayer > DistanceToPursue);
    }
    #endregion
    
    protected override void Update()
    {
        if (gm.CurrentState == GameManager.GMState.GAME)
        {
            distanceFromPlayer = Vector2.Distance(transform.position, playerTransform.position);
        }
        base.Update();
    }
}
