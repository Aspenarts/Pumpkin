using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PumpkinController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    [SerializeField] private float rotationSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void RotateDirection(){
        // Rotate pumpkin towards moving direction
        transform.Rotate(0,0,-moveDirection.x*rotationSpeed);
        transform.Rotate(0,0,-moveDirection.y*rotationSpeed);
    }
    void Update()
    {
        if(rb.velocity != Vector2.zero){
            moveDirection = rb.velocity;
            RotateDirection();
        }
    }
}
