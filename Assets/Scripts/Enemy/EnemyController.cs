using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class EnemyController : MonoBehaviour
{
    // Available States
    public IdleState idleState;
    public FollowState followState;
    public AttackState attackState;

    // Current State
    private EnemyState currentState;

    // Reference to the object
    public Transform Player;
    public GameObject prefabStone;
    public Transform FirePoint;
    public Rigidbody rb {private set; get;}
    // Distance to start following
    public float distanceToFollow = 4f;
    public float distanceToAttack = 3f;
    public float speed = 1f;

    private void Awake()
    {
        idleState = new IdleState(this);
        followState = new FollowState(this);
        attackState = new AttackState(this);

        rb = GetComponent<Rigidbody>();

        // Initial state
        currentState = idleState;
    }

    private void Start()
    {
        currentState.OnStart();
    }
    private void Update()
    {
        foreach (var transition in currentState.transitions)
        {
            if (transition.IsValid())
            {
                // Execute transition
                currentState.OnFinish();
                currentState = transition.GetNextState();
                currentState.OnStart();
                break;
            }
        }
        currentState.OnUpdate();
    }

    public void Fire() {
        GameObject stone = Instantiate(prefabStone, FirePoint.position, Quaternion.identity);
        stone.GetComponent<StoneMovement>().stoneDirection = (Player.position - transform.position).normalized;

    }

}
