using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PumpkinController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Quaternion rotation;
    [SerializeField] private float rotationSpeed = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void RotateDirection(){
        transform.Rotate(0,0,moveDirection.x);
    }

    private void isMoving(){
        
    }
    void Update()
    {
        moveDirection = transform.forward;
        RotateDirection();
    }
}
