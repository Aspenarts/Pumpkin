using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class AttackState : State
{
    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;
    public float speed = 25f;
    public float attackRadius = 5f; // Radius for triggering the attack
    private GameObject target;
    private float distanceToPlayer; // Distance between goose and player

    public AttackState(GameObject owner, GameObject target) : base(owner)
    {
        seeker = owner.GetComponent<Seeker>();
        rb = owner.GetComponent<Rigidbody2D>();
        this.target = target; // Assign the player reference
    }

    public override void OnEnter()
    {
        Debug.Log("Goose started chasing the player.");
        MoveToPlayer(); // Start moving toward the player
    }

    public override void OnUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Player object not set.");
            return;
        }

        // Calculate distance to the player
        distanceToPlayer = Vector2.Distance(rb.position, target.transform.position);

        if (distanceToPlayer <= attackRadius)
        {
            AttackPlayer(); // Trigger the attack when within radius
            return; // Stop further movement
        }

        // Ensure the path is valid
        if (path == null || path.vectorPath == null || path.vectorPath.Count == 0)
        {
            return; // No valid path
        }

        // Move towards the next waypoint
        if (currentWaypoint >= path.vectorPath.Count)
        {
            MoveToPlayer(); // Recalculate path if reached the last waypoint
            return;
        }

        // Move towards the player
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.MovePosition(rb.position + force);

        // Check if the goose is close enough to the current waypoint to move to the next one
        if (Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]) < 0.2f)
        {
            currentWaypoint++;
        }
    }

    public override void OnExit()
    {
        Debug.Log("Goose stopped chasing the player.");
        rb.velocity = Vector2.zero; // Stop movement when exiting the state
    }

    public override List<Transition> GetTransitions()
    {
        // Return any transitions (e.g., if player goes out of attack range, transition back to PatrolState)
        return new List<Transition> { };
    }

    private void MoveToPlayer()
    {
        // Request a path to the player�s current position
        seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Debug.Log("Path to player calculated");
            path = p;
            currentWaypoint = 0; // Start from the first waypoint
        }
        else
        {
            Debug.LogError("Error calculating path to player: " + p.errorLog);
        }
    }

    private void AttackPlayer()
    {
        // Perform the attack (play animation, reduce health, etc.)
       // Debug.Log("Goose attacks the player!");
        // Add your attack logic here (e.g., destroy the player object or reduce health)
    }
}