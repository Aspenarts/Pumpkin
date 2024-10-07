using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float dirX, dirY;
    Rigidbody2D rb;
    public float speed;

    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        print(rb.velocity);
        rb.velocity = new Vector2(dirX, dirY).normalized * speed;
    }
}
