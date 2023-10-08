using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStart()
    {
        Debug.Log("OnStart IdleState");
    }
    public override void OnUpdate()
    {
        //Debug.Log("OnUpdate IdleState");
    }
    public override void OnFinish()
    {
        Debug.Log("OnFinish IdleState");
    }

}
