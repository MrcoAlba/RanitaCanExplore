using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStart()
    {
        Debug.Log("OnStart AttackState");
    }
    public override void OnUpdate()
    {
        //Debug.Log("OnUpdate AttackState");
    }
    public override void OnFinish()
    {
        Debug.Log("OnFinish AttackState");
    }

}
