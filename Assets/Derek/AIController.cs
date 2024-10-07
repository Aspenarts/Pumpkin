using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private StateMachine stateMachine;
    public GameObject player; // Assign this in the inspector or programmatically
    public float patrolRadius = 10f; // Set patrol radius in the Inspector

    private void Start()
    {
        stateMachine = new StateMachine();
        PatrolState patrolState = new PatrolState(gameObject, player, patrolRadius);
        stateMachine.SetState(patrolState);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}