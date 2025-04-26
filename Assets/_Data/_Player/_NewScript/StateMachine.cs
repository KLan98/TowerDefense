using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handle init state and changing states
public class StateMachine 
{
    public State currentState;

    public void Initialize(State startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        // exit current state
        currentState.Exit();    

        // assign new state 
        currentState = newState;
        newState.Enter(); // enter new state
    }
}
