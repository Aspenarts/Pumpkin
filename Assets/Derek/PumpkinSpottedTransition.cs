using System.Collections.Generic;
using UnityEngine;

public class PumpkinSpottedTransition : Transition
{
    private List<GameObject> pumpkins;  // List of all pumpkins
    private GameObject owner;    // Reference to the enemy (goose)
    private VisionCone visionCone; // Reference to the vision cone component

    public PumpkinSpottedTransition(GameObject owner, List<GameObject> pumpkins) : base(owner)
    {
        this.owner = owner;
        this.pumpkins = pumpkins;
        this.visionCone = owner.GetComponent<VisionCone>(); // Get the vision cone component from the enemy
    }

    public override bool ShouldTransition()
    {
        // Check if any pumpkin is detected in the vision cone
        foreach (var pumpkin in pumpkins)
        {
            if (pumpkin != null && visionCone.IsTargetInVision(pumpkin))
            {
                Debug.Log("Pumpkin detected: " + pumpkin.name);
                return true; // Transition to attack state
            }
        }

        return false; // No pumpkin detected, no transition
    }




    public override State GetNextState()
    {
        foreach (GameObject pumpkin in pumpkins)
        {
            if (pumpkin != null && visionCone.IsTargetInVision(pumpkin))
            {
                Debug.Log("Switching to AttackState targeting: " + pumpkin.name);
                return new AttackState(owner, pumpkin); // Attack the detected pumpkin
            }
        }

        return null; // This should never happen since we check in ShouldTransition()
    }

}
