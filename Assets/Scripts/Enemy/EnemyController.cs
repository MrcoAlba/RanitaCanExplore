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
    public Rigidbody rb { private set; get; }
    // Distance to start following
    public float distanceToFollow = 4f;
    public float distanceToAttack = 3f;
    public float speed = 1f;

    [SerializeField] private GameObject playerBody;
    [SerializeField] private GameObject healthBar;

    private float pushTimer = 0f;

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
        
        if (healthBar.GetComponent<Slider>().value <= 0.01)
        {
            // Debug.Log("HOLI ESTOY AQUI");
            gameObject.SetActive(false);
        }
        else{
            // Debug.Log("HOLI ADIOS:" + healthBar.GetComponent<Slider>().value);
        }



        if(pushTimer>0){
            pushTimer -= Time.deltaTime;
            pushEnemy();
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

    public void Fire()
    {
        GameObject stone = Instantiate(prefabStone, FirePoint.position, Quaternion.identity);
        stone.GetComponent<StoneMovement>().stoneDirection = (Player.position - transform.position).normalized;

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player") && playerBody.GetComponent<PlayerMovement>().dmgCanvasTimer > 0)
        {
            pushTimer = 2f;
            healthBar.GetComponent<Slider>().value -= 0.2f;
            pushEnemy();
        }
    }



    private void pushEnemy()
    {
        if (playerBody.GetComponent<PlayerMovement>().attackOption == 0)
        {
            moveEnemy(2);
        }
        else if (playerBody.GetComponent<PlayerMovement>().attackOption == 1)
        {
            moveEnemy(6);
        }
    }

    private void moveEnemy(int force)
    {
        if (playerBody.GetComponent<PlayerMovement>().moveDir.x != 0)
        {
            if (playerBody.GetComponent<PlayerMovement>().moveDir.x > 0)
            {
                rb.AddForce(0, force, 0, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(0, -force, 0, ForceMode.Impulse);
            }
        }
        else if (playerBody.GetComponent<PlayerMovement>().moveDir.y != 0)
        {
            if (playerBody.GetComponent<PlayerMovement>().moveDir.y > 0)
            {
                rb.AddForce(force, 0, 0, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(-force, 0, 0, ForceMode.Impulse);
            }
        }
        else
        {
            rb.AddForce(0, 0, force, ForceMode.Impulse);
        }
    }
}
