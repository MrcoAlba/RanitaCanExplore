using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public IdleState idleState;
    public FollowState followState;
    public AttackState attackState;

    private EnemyState currentState;

    private void Awake(){
        idleState   = new IdleState(this)  ;
        followState = new FollowState(this);
        attackState = new AttackState(this);

        currentState = idleState;
    }

    private void Start(){
        currentState.OnStart();
    }
    private void Update(){
        currentState.OnUpdate();
    }

}
