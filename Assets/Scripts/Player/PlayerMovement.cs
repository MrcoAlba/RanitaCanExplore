using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector3 moveSpeed = new(2f, 1f, 2f); // new (X, Y, Z)
    [SerializeField] private Vector3 jumpSpeed = new(1f, 3f, 1f); // new (X, Y, Z)
    private Rigidbody rb;
    private Animator animator;
    private PlayerInput playerInput;
    public Vector2 moveDir;
    private Vector3 spawnPoint;

    // Damage related
    [SerializeField] private GameObject dmgCanvas;
    [SerializeField] private TMP_Text dmgCanvasText;
    public float dmgCanvasTimer = 0f;

    // Attack related
    [SerializeField] private GameObject attackMenuUi;
    private int attackDmg = 2;
    public int attackOption = 0;

    // // Enemy related
    // [SerializeField] private GameObject enemyBody;
    // [SerializeField] private GameObject enemyHealthBar;

    // Player life related
    [SerializeField] private GameObject playerHealthBar;

    private bool attacked = false;


    private bool isJumping;
    #endregion

























































    // Reference components from this script
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        dmgCanvas.SetActive(false);
        spawnPoint = transform.position;
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
        // If it's death, kill the player
        if (playerHealthBar.GetComponent<Slider>().value <= 0)
        {
            gameObject.SetActive(false);
        }

        // If attackMenu isn't open, the player can move
        if (attackMenuUi.GetComponent<AttackMenuUI>().isMenuOpen == false)
        {
            rb.velocity = new Vector3(
            moveDir.x * moveSpeed.x,// This is the X axis - moveDir is 2D
            rb.velocity.y * moveSpeed.y,// This is the Y axis - keeps gravity
            moveDir.y * moveSpeed.z // This is the Z axis - moveDir is 2D
            );
        }


        attackOption = attackMenuUi.GetComponent<AttackMenuUI>().selection;

        dmgCanvasText.text = "+" + attackDmg.ToString();

        // TODO BIEN

        if (dmgCanvasTimer > 0f)
        {
            dmgCanvas.SetActive(true);
            dmgCanvasTimer -= Time.deltaTime;
        }
        else
        {
            dmgCanvas.SetActive(false);
            animator.SetBool("IsAttacking1", false);
            animator.SetBool("IsAttacking2", false);
        }
    }

    private void OnDialogueStartDelegate(Interaction interaction)
    {
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
            if (dmgCanvasTimer <= 0f)
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsAttacking1", false);
                animator.SetBool("IsAttacking2", false);
            }
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

    private void OnAttack(InputValue value)
    {
        // TODO JD: Add a cooldown
        if (dmgCanvasTimer <= 0)
        {
            if (attackOption == 0)
            {
                dmgCanvasTimer = 1f;
                animator.SetBool("IsAttacking1", true);
                animator.SetBool("IsAttacking2", false);
                attackDmg = 2;
            }
            else
            {
                dmgCanvasTimer = 1f;
                animator.SetBool("IsAttacking1", false);
                animator.SetBool("IsAttacking2", true);
                attackDmg = 4;
            }
        }
    }

    private void OnJumping()
    {
        Debug.Log("HOLI, SALTO :)");
        if (!isJumping)
        {
            rb.velocity = new Vector3(
                        moveDir.x * jumpSpeed.y,// This is the X axis - keeps velocity
                        jumpSpeed.y,// This is the Y axis
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


    private void OnCollisionEnter(Collision other)
    {
        Dialogue dialogue = other.collider.transform.GetComponent<Dialogue>();
        if (dialogue != null)
        {
            // Start dialogue
            DialogueManager.Instance.StartDialogue(dialogue);
        }

        if (other.transform.CompareTag("Enemy") && dmgCanvasTimer <= 0)
        {
            playerHealthBar.GetComponent<Slider>().value -= 0.2f;
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        else if (other.gameObject.CompareTag("Respawn"))
        {
            this.transform.position = spawnPoint;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }


}
