using UnityEngine;
using System.Collections.Generic;  // For using List

public class AIController : MonoBehaviour
{
    private StateMachine stateMachine;
    public GameObject player;   // Assign this in the Inspector or dynamically
    public List<GameObject> pumpkins;  // List to store all spawned pumpkins
    public GameObject enemy;    // Reference to the enemy

    void Start()
    {
        // Initialize the state machine
        stateMachine = new StateMachine();

        // Set the initial state (PatrolState), passing the player and pumpkins
        PatrolState patrolState = new PatrolState(enemy, player, pumpkins);
        stateMachine.SetState(patrolState);

        // Add transitions here if necessary
    }

    void Update()
    {
        stateMachine.Update();  // Make sure to update the state machine each frame
    }

    // New method to set all pumpkin targets
    public void SetPumpkinTargets(List<GameObject> newPumpkins)
    {
        pumpkins = newPumpkins;  // Store all the pumpkins
        Debug.Log("Pumpkin targets set for the AI. Total Pumpkins: " + pumpkins.Count);
    }
}
