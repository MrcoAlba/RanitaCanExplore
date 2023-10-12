using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(SphereCollider))]
public class StoneMovement : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] private float stoneSpeed = 5f;
    [SerializeField] private float timeToDestroy = 3f;

    private Rigidbody rb;
    private float timer = 0f;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        timer += Time.deltaTime;
    }

    private void Start(){
        rb.velocity = direction.normalized * stoneSpeed;
    }

    private void OnCollisionEnter(Collision other){

    }
}
