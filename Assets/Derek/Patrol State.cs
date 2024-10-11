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
    private GameObject pumpkin;
    private VisionCone visionCone; // Reference to the VisionCone component

    public PatrolState(GameObject owner, GameObject player, GameObject pumpkin) : base(owner)
    {
        seeker = owner.GetComponent<Seeker>();
        rb = owner.GetComponent<Rigidbody2D>();
        this.player = player; // Assign the player reference
        this.pumpkin = pumpkin;
        this.visionCone = owner.GetComponent<VisionCone>(); // Get the vision cone component
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
            return; // No valid path
        }

        // Move towards the next waypoint
        if (currentWaypoint >= path.vectorPath.Count)
        {
            MoveToRandomPoint(); // Recalculate path if reached the last waypoint
            return;
        }

        // Calculate movement direction to the next waypoint
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.MovePosition(rb.position + force);

        // Check if the enemy is moving left or right and update vision cone direction
        bool isMovingRight = direction.x > 0;
        visionCone.SetFacingDirection(isMovingRight);

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
        // Return both PlayerSpottedTransition and PumpkinSpottedTransition
        return new List<Transition>
    {
        new PlayerSpottedTransition(owner, player),
        new PumpkinSpottedTransition(owner, pumpkin)
    };
    }


    private void MoveToRandomPoint()
    {
        // Generate a random point within a 2D circle
        Vector3 randomDirection = Random.insideUnitCircle * patrolRadius;
        randomDirection += Vector3.zero; // Fixed patrol around (0,0)

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
