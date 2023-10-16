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
        enemyController.Fire();
    }
    public override void OnUpdate()
    {
        
    }
    public override void OnFinish()
    {
        Debug.Log("OnFinish AttackState");
    }

}
