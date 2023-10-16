using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector3 moveSpeed = new(2f, 1f, 2f); // new (X, Y, Z)
    [SerializeField] private Vector3 jumpSpeed = new(1f, 3f, 1f); // new (X, Y, Z)
    private Rigidbody rb;
    private Animator animator;
    private PlayerInput playerInput;
    private Vector2 moveDir;

    [SerializeField] private GameObject dmgCanvas;
    [SerializeField] private TMP_Text dmgCanvasText;

    public float dmgCanvasTimer=0f;

    [SerializeField] private GameObject attackMenuUi;
    

    private int attackDmg = 2;

    private int attackOption = 0;

    [SerializeField] private GameObject enemyBody;
    [SerializeField] private GameObject enemyHealthBar;

    [SerializeField] private GameObject playerHealthBar;

    private float pushTimer=0f;

    private bool attacked=false;


    private bool isJumping;
    #endregion

    // Reference components from this script
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        dmgCanvas.SetActive(false);
    }

    // Reference components from other script
    void Start()
    {
        DialogueManager.Instance.OnDialogueStart += OnDialogueStartDelegate;
        DialogueManager.Instance.OnDialogueFinish += OnDialogueFinishDelegate;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealthBar.GetComponent<Slider>().value<=0){
            gameObject.SetActive(false);
        }

        if(attackMenuUi.GetComponent<AttackMenuUI>().isMenuOpen==false){
            rb.velocity = new Vector3(
            moveDir.x     * moveSpeed.x,// This is the X axis - moveDir is 2D
            rb.velocity.y * moveSpeed.y,// This is the Y axis - keeps gravity
            moveDir.y     * moveSpeed.z // This is the Z axis - moveDir is 2D
        );
        }

        attackOption=attackMenuUi.GetComponent<AttackMenuUI>().selection;

        dmgCanvasText.text= "+"+attackDmg.ToString();

        Debug.Log(moveDir);
        
        if(dmgCanvasTimer>0f){
            dmgCanvas.SetActive(true);
            dmgCanvasTimer-=Time.deltaTime;
        }else{
            dmgCanvas.SetActive(false);
            animator.SetBool("IsAttacking1", false);
            animator.SetBool("IsAttacking2", false);
        }

        if(pushTimer>0f){
            pushEnemy();
            pushTimer-=Time.deltaTime;
        }else{
            attacked=false;
        }
    }

    private void OnDialogueStartDelegate(Interaction interaction) {
        playerInput.SwitchCurrentActionMap("Dialogue");
    }

    private void OnDialogueFinishDelegate()
    {
        // Change input map to player mode
        playerInput.SwitchCurrentActionMap("Player");
    }

    private void OnMovement(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        // if moveDir isn't normalize, when you move up and right,
        // the velocity will be 1 for Y and 1 for X.
        moveDir.Normalize();
        // Animations for "walking" or "idling"
        if (Mathf.Abs(moveDir.x) > Mathf.Epsilon ||
            Mathf.Abs(moveDir.y) > Mathf.Epsilon)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsAttacking1", false);
            animator.SetBool("IsAttacking2", false);
            animator.SetFloat("Horizontal", moveDir.x);
            animator.SetFloat("Vertical", moveDir.y);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            // Horizontal and Vertical aren't changed here bc we 
            // want to know where was the last direction the player
            // was going to set the idle direction animation
        }

    }

    private void OnAttack(InputValue value){
        if(attackOption==0){
            dmgCanvasTimer=1f;
            animator.SetBool("IsAttacking1", true);
            animator.SetBool("IsAttacking2", false);
            attackDmg=2;
        }else{
            dmgCanvasTimer=1f;
            animator.SetBool("IsAttacking1", false);
            animator.SetBool("IsAttacking2", true);
            attackDmg=4;
        }
    }

    private void OnJumping()
    {
        Debug.Log("HOLI, SALTO :)");
        if (!isJumping)
        {
            rb.velocity = new Vector3(
                        moveDir.x * jumpSpeed.y,// This is the X axis - keeps velocity
                        jumpSpeed.y            ,// This is the Y axis
                        moveDir.y * jumpSpeed.z // This is the Z axis - keeps velocity
                    );
            isJumping = true;
        }
    }

    private void OnNextInteraction(InputValue value)
    {
        if (value.isPressed)
        {
            // Next dialogue
            DialogueManager.Instance.NextDialogue();
        }
    }


    private void OnCollisionEnter(Collision other) {
        Dialogue dialogue = other.collider.transform.GetComponent<Dialogue>();
        if(dialogue!=null){
            // Start dialogue
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        if(other.transform.CompareTag("Enemy") && dmgCanvasTimer>0){
            enemyBody.GetComponent<EnemyController>().enemyHealthBar-=attackDmg;
            enemyHealthBar.GetComponent<Slider>().value-=(attackDmg/10f);
            pushTimer=0.4f;
        }else if(other.transform.CompareTag("Enemy")){
            pushTimer=0.4f;
            attacked=true;
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void pushEnemy(){
        if(attackOption==0){
            moveEnemy(3);
        }else if(attackOption==1){
            moveEnemy(5);
        }else if(attacked){
            moveEnemy(1);
        }
    }

    private void moveEnemy(int force){
            if(moveDir.x!=0){
                if(moveDir.x>0){
                    enemyBody.GetComponent<Rigidbody>().AddForce(0,force,0,ForceMode.Impulse);
                }else{
                    enemyBody.GetComponent<Rigidbody>().AddForce(0,-force,0,ForceMode.Impulse);
                }
            }else if(moveDir.y!=0){
                if(moveDir.y>0){
                    enemyBody.GetComponent<Rigidbody>().AddForce(force,0,0,ForceMode.Impulse);
                }else{
                    enemyBody.GetComponent<Rigidbody>().AddForce(-force,0,0,ForceMode.Impulse);
                }
            }else{
                enemyBody.GetComponent<Rigidbody>().AddForce(0,0,force,ForceMode.Impulse);
            }
    }
}
