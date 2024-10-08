using UnityEngine;

public class AIController : MonoBehaviour
{
    private StateMachine stateMachine;
    public GameObject player; // Assign the player GameObject in the Inspector
    private PatrolState patrolState;

    private void Start()
    {
        stateMachine = new StateMachine();

        // Initialize PatrolState with only the owner (enemy) and player arguments
        patrolState = new PatrolState(gameObject, player);

        // Set the patrol state as the starting state
        stateMachine.SetState(patrolState);
    }

    private void Update()
    {
        // Update the state machine every frame
        stateMachine.Update();
    }
}
