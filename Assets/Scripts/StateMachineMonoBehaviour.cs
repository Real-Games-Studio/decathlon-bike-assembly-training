using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineMonoBehaviour<TState>: MonoBehaviour, IStateMachine<TState> where TState : Enum
{
    public TState currentState { get; set; }

    public void ChangeState(TState newState)
    {
        OnExitState();
        currentState = newState;
        OnEnterState(newState);
    }

    protected virtual void OnExitState() //Executes exit comands of the current state
    {

    }

    protected virtual void OnEnterState(TState newState) //Executes enter comands of the new state
    {
 
    }
}
