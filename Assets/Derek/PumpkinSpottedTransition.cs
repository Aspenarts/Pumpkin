using UnityEngine;

public class PumpkinSpottedTransition : Transition
{
    private GameObject pumpkin;  // Reference to the pumpkin
    private GameObject owner;    // Reference to the enemy (goose)
    private VisionCone visionCone; // Reference to the vision cone component

    public PumpkinSpottedTransition(GameObject owner, GameObject pumpkin) : base(owner)
    {
        this.owner = owner;
        this.pumpkin = pumpkin;
        this.visionCone = owner.GetComponent<VisionCone>(); // Get the vision cone component from the enemy
    }

    public override bool ShouldTransition()
    {
        // Check if the pumpkin is inside the vision cone (no need to check for movement)
        if (pumpkin != null && visionCone.IsTargetInVision(pumpkin))
        {
            Debug.Log("Player Spotted Transition.");

            return true; // Transition to attack state if the pumpkin is in the vision cone
        }

        return false; // No transition if the pumpkin is not in the vision cone
    }

    public override State GetNextState()
    {
        // Transition to the AttackState and pass the pumpkin as the target
        return new AttackState(owner, pumpkin); // Attack the pumpkin
    }
}
