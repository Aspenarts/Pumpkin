using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    //player speed

    public float sprintmod;
    //sprinting modifier

    public float rollmod;
    //rolling modifier

    public float anglemod;
    //angle that the camera is at, dampens Y speed

    float dirX, dirY;
    //direction of player determined by arrow keys / wasd

    Vector3 rotationdirection;
    bool ishiding = false;
    bool issprinting = false;
    bool isrolling = false;
    bool stoprolling = false;
    Rigidbody2D rb;
    SpriteRenderer basesprite;

    SpriteRenderer hidesprite;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        basesprite = transform.Find("basesprite").GetComponent<SpriteRenderer>();
        hidesprite = transform.Find("hidesprite").GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        //print(issprinting);
        if (ishiding == false) {
            

            //print(rb.velocity);
            if (Input.GetKey(KeyCode.LeftShift))
            //playersprint
            {
                rb.velocity = new Vector2(dirX + anglemod * dirX, dirY).normalized * speed * sprintmod;
                issprinting = true;
            }
            else
            {
                issprinting = false;
                rb.velocity = new Vector2(dirX + anglemod * dirX, dirY).normalized * speed;
            }
            
            
            
        }
       
        if (Input.GetKeyDown(KeyCode.J))
        //playerhide
        {

            basesprite.enabled = false;
            hidesprite.enabled = true;
            
            //player is hiding
            ishiding = true;
            //do something to hinder player velocity
            if (issprinting == false)
            {

                //print("not spritning");
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                isrolling = true;
                
                rb.velocity = new Vector2(dirX + anglemod * dirX, dirY).normalized * speed * sprintmod * rollmod;

                rotationdirection = Vector3.back;
                if (dirX != 0)
                {
                    rotationdirection = Vector3.back * dirX;
                }
            }
            
        }
        if (Input.GetKeyUp(KeyCode.J)){
            basesprite.enabled = true;
            hidesprite.enabled = false;

            
            ishiding = false;
            
            isrolling = false;
            hidesprite.gameObject.transform.eulerAngles = new Vector3(0,0,0);
        }
        if (isrolling == true)
        {
            
            
            hidesprite.gameObject.transform.Rotate(rotationdirection * 1f);
        }
       

        
       
       
    }
}
