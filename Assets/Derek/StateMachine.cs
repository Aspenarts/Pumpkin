using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State currentState;

    public void Update()
    {
        // Update current state
        currentState?.OnUpdate();

        // Check for transitions and move to a new state if necessary
        foreach (var transition in currentState.GetTransitions())
        {
            if (transition.ShouldTransition())
            {
                SetState(transition.GetNextState());
                break;
            }
        }
    }

    public void SetState(State newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState?.OnEnter();
    }
}