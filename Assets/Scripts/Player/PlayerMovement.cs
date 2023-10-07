using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new (4f, 4f);
    private Rigidbody rb;
    private Vector2 moveDir;

    // Reference components from this script
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            0f,                 // This is the Y axis
            moveDir.y * speed.y // This is the Z axis
        );
    }

    private void OnMovement(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

}
