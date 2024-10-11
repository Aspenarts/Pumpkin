using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 1.5f;
    [SerializeField] private LayerMask layerToCheck;
    [SerializeField] private bool hasPumpkin = false;
    RaycastHit2D rayCheck;
    Vector2 movement;
    Vector2 lastMoveDir;
    

    private void HandleInteract(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(movement != Vector2.zero){
            lastMoveDir = movement;
        }
        rayCheck = Physics2D.Raycast(transform.position, lastMoveDir, interactDistance, layerToCheck);
        if(rayCheck.collider != null && Input.GetKeyDown(KeyCode.E) && !hasPumpkin){
            Debug.DrawRay(transform.position, lastMoveDir*interactDistance, Color.blue);
            Debug.Log(rayCheck.collider.gameObject.name + " was hit");
            rayCheck.collider.gameObject.transform.position = transform.position;
            rayCheck.collider.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            rayCheck.collider.gameObject.transform.parent = this.transform;
        } else{
            Debug.DrawRay(transform.position, lastMoveDir*interactDistance, Color.white);
        }
    }
    void Update()
    {
        HandleInteract();
    }
}
