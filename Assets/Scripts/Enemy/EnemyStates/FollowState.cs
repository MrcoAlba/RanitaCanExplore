using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : EnemyState
{
    public FollowState(EnemyController enemyController) : base(enemyController)
    {
        // Creating transition from Follow -> Idle
        Transition transitionFollowToIdle = new Transition(
            isValid: () => {
                float distance = Vector3.Distance(
                    enemyController.Player.position,
                    enemyController.transform.position
                    );
                if (distance >= enemyController.distanceToFollow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            },
            getNextState: () => {
                return new IdleState(enemyController); 
            }
        );
    }

    public override void OnStart()
    {
        Debug.Log("OnStart FollowState");
    }
    public override void OnUpdate()
    {
        Vector3 dir = (
            enemyController.Player.position - enemyController.transform.position
        ).normalized;
        enemyController.rb.velocity = dir * enemyController.speed;
    }
    public override void OnFinish()
    {
        Debug.Log("OnFinish FollowState");
    }

}
