using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpottedTransition : Transition
{
    private GameObject player;

    // Constructor that takes two arguments: owner and player
    public PlayerSpottedTransition(GameObject owner, GameObject player) : base(owner)
    {
        this.player = player;
    }

    public override bool ShouldTransition()
    {
        // Check if the player exists
        if (player == null) return false;

        // Transition to attack if the player is within a certain distance
        return Vector3.Distance(owner.transform.position, player.transform.position) < 10f;
    }

    public override State GetNextState()
    {
        // Return the next state (e.g., AttackState)
        return new AttackState(owner);
    }
}
