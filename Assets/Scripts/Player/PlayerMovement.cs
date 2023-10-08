using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLES ---------------------------------------------------------------
    [SerializeField] private Vector3 speed = new (4f, 0f, 4f); // new (X, Y, Z)
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
            0f        * speed.y,// This is the Y axis
            moveDir.y * speed.z // This is the Z axis
        );
    }

    private void OnMovement(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        animator.SetFloat("Horizontal", moveDir.x);
        animator.SetFloat("Vertical", moveDir.y);
    }

}
