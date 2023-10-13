using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector3 moveSpeed = new(2f, 1f, 2f); // new (X, Y, Z)
    [SerializeField] private Vector3 jumpSpeed = new(1f, 3f, 1f); // new (X, Y, Z)
    private Rigidbody rb;
    private Animator animator;
    private PlayerInput playerInput;
    private Vector2 moveDir;
    private bool isJumping;
    #endregion

    // Reference components from this script
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
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
        rb.velocity = new Vector3(
            moveDir.x     * moveSpeed.x,// This is the X axis - moveDir is 2D
            rb.velocity.y * moveSpeed.y,// This is the Y axis - keeps gravity
            moveDir.y     * moveSpeed.z // This is the Z axis - moveDir is 2D
        );
    }

    private void OnDialogueStartDelegate(Interaction interaction)
    {
        // Change input map to dialogue mode
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


    private void OnCollisionEnter(Collision other)
    {
        Dialogue dialogue = other.collider.transform.GetComponent<Dialogue>();
        if (dialogue != null)
        {
            // Start dialogue
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

}
