using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


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

    [SerializeField] private GameObject playerBody;
    [SerializeField] private GameObject healthBar;

    public int enemyHealthBar=10;

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
        if(enemyHealthBar<=0){
            gameObject.SetActive(false);
        }

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

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Player")){
            if(playerBody.GetComponent<PlayerMovement>().dmgCanvasTimer<=0f){
                healthBar.GetComponent<Slider>().value-=0.2f;
                Debug.Log("oh no choco");
            }
        }
    }

}
