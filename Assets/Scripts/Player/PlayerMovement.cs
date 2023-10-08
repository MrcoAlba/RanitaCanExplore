using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLES ---------------------------------------------------------------
    [SerializeField] private Vector3 speed = new(2f, 0f, 2f); // new (X, Y, Z)
    private Rigidbody rb;
    private Animator animator;
    private Vector2 moveDir;
    // VARIABLES ---------------------------------------------------------------



    // Reference components from this script
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Reference components from other script
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        rb.velocity = new Vector3(
            moveDir.x * speed.x,// This is the X axis
            0f * speed.y,// This is the Y axis
            moveDir.y * speed.z // This is the Z axis
        );
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

}
