using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(SphereCollider))]
public class StoneMovement : MonoBehaviour
{
    public Vector3 stoneDirection;
    [SerializeField] private float stoneSpeed = 5f;
    [SerializeField] private float timeToDestroy = 3f;
    private float timer = 0f;
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = stoneDirection.normalized * stoneSpeed;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            DestroyStone();
        }
    }

    
    private void OnCollisionEnter(Collision other)
    {
        // If there is a collision, destroy the stone
        Debug.Log("TOCO ALGO LA PIEDRA");
        DestroyStone();
    }

    private void DestroyStone()
    {
        Destroy(gameObject);
    }
}
