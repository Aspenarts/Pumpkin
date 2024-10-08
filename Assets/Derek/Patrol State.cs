using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class PatrolState : State
{
    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;
    public float speed = 10f;
    public float patrolRadius = 50f; // Patrol radius for random roaming
    private GameObject player; // Reference to the player

    public PatrolState(GameObject owner, GameObject player) : base(owner)
    {
        seeker = owner.GetComponent<Seeker>();
        rb = owner.GetComponent<Rigidbody2D>();
        this.player = player; // Assign the player reference
    }

    public override void OnEnter()
    {
        Debug.Log("Enemy started random patrol.");
        MoveToRandomPoint(); // Move to the first random point
    }

    public override void OnUpdate()
    {
        // Ensure the path is valid
        if (path == null || path.vectorPath == null || path.vectorPath.Count == 0)
        {
            // No valid path, return early
            return;
        }

        // Check if the current waypoint index is within the valid range
        if (currentWaypoint >= path.vectorPath.Count)
        {
            // Reached the end of the path, request a new path immediately
            MoveToRandomPoint();
            return;
        }

        // Move towards the next waypoint
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.MovePosition(rb.position + force);

        // Check if the enemy is close enough to the current waypoint to move to the next one
        if (Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]) < 0.2f)
        {
            currentWaypoint++;
        }
    }




    public override void OnExit()
    {
        Debug.Log("Enemy stopped patrolling.");
        rb.velocity = Vector2.zero; // Stop movement when exiting the state
    }

    public override List<Transition> GetTransitions()
    {
        // Return a list of possible transitions, such as transitioning to a chase state if the player is spotted
        return new List<Transition> { new PlayerSpottedTransition(owner, player) };
    }

    private void MoveToRandomPoint()
    {
        // Generate a random point within a 2D circle
        Vector3 randomDirection = Random.insideUnitCircle * patrolRadius;
        randomDirection += owner.transform.position; // Offset from the enemy's current position

        // Request a path to the random point
        seeker.StartPath(rb.position, randomDirection, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Debug.Log("Path successfully calculated");
            path = p;
            currentWaypoint = 0; // Start from the first waypoint
        }
        else
        {
            Debug.LogError("Error calculating path: " + p.errorLog);
        }
    }
}
