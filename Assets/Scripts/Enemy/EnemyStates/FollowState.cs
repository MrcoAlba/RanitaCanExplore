using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : EnemyState
{
    public FollowState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStart()
    {
        Debug.Log("OnStart FollowState");
    }
    public override void OnUpdate()
    {
        //Debug.Log("OnUpdate FollowState");
    }
    public override void OnFinish()
    {
        Debug.Log("OnFinish FollowState");
    }

}
