using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private GameObject player;
    private NavMeshAgent navAgent;
    private float patrolRadius;

    public PatrolState(GameObject owner, GameObject player, float patrolRadius) : base(owner)
    {
        this.player = player;
        this.patrolRadius = patrolRadius;
        navAgent = owner.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        Debug.Log("Enemy started patrolling randomly.");
        MoveToRandomPoint();
    }

    public override void OnUpdate()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            MoveToRandomPoint();
        }
    }

    public override void OnExit()
    {
        Debug.Log("Enemy stopped patrolling.");
        navAgent.ResetPath();
    }

    public override List<Transition> GetTransitions()
    {
        // Passing both the owner and player to PlayerSpottedTransition
        return new List<Transition> { new PlayerSpottedTransition(owner, player) };
    }

    private void MoveToRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += owner.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            navAgent.SetDestination(hit.position);
        }
    }
}
