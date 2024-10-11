using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerSpottedTransition : Transition
{
    private GameObject player;  // Reference to the player
    private GameObject owner;   // Reference to the enemy (goose)
    private VisionCone visionCone; // Reference to the vision cone component

    public PlayerSpottedTransition(GameObject owner, GameObject player) : base(owner)
    {
        this.owner = owner;
        this.player = player;
        this.visionCone = owner.GetComponent<VisionCone>(); // Get the vision cone component from the enemy
    }

    public override bool ShouldTransition()
    {
        // Check if the player is inside the vision cone and moving
        if (player != null && visionCone.IsTargetInVision(player))
        {
            MovementDetector movementDetector = player.GetComponent<MovementDetector>();

            // Check if the player is moving
            if (movementDetector != null && movementDetector.isMoving)
            {
                Debug.Log("Player Spotted Transition.");

                return true; // Player is in the vision cone and moving, so transition to attack state
            }
        }

        return false; // No transition if player isn't in the cone or isn't moving
    }

    public override State GetNextState()
    {
        // Transition to the AttackState and pass both owner and player
        return new AttackState(owner, player);
    }
}
