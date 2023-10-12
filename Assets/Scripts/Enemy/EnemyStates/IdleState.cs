using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(EnemyController enemyController) : base(enemyController)
    {
        // Creating transition from Idle -> Follow
        Transition transitionIdleToAttack = new Transition(
            isValid: () =>
            {
                float distance = Vector3.Distance(
                    enemyController.Player.position,
                    enemyController.transform.position
                    );
                if (distance < enemyController.distanceToFollow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            },
            getNextState: () =>
            {
                return new FollowState(enemyController);
            }
        );

        // Adding transitions to our list
        transitions.Add(transitionIdleToAttack);
    }

    public override void OnStart()
    {
        Debug.Log("OnStart IdleState");
        enemyController.rb.velocity = Vector3.zero;
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
