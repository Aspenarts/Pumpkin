using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(GameObject owner) : base(owner) { }

    public override void OnEnter()
    {
        // Start attacking
    }

    public override void OnUpdate()
    {
        // Attack logic
    }

    public override void OnExit()
    {
        // Clean up attack state
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>(); // Define necessary transitions
    }
}