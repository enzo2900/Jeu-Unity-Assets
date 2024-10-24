using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{

    //private HashSet<State> states = new HashSet<State>();

    private IState currentState;
    private List<State> globalStates;
    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    public void Update()
    {
        VerifyNextStates();
        currentState.Update();
    }
    public void setState(IState state)
    { 
        currentState = state;
        currentState.OnEnter();
    }
    public void SetInitialState(State initialState)
    {
        this.currentState = initialState;
        currentState.OnEnter();
    }

    /*bool VerifyGlobalStates()
    {
        if (globalStates.Count == 0) {
            return false;
        }
        IState nextState = null;
        foreach (var state in globalStates)
        {
            nextState = state.NextState();
        }
        if (nextState != null)
        {
            return true;
        }
        return false;

    }*/

    void VerifyNextStates()
    {
        /* if(VerifyGlobalStates())
        {
            return;
        }*/
        while (true)
        {
            
             IState nextState = currentState.NextState();
            if (nextState != null)
            {
                setState(nextState);
            }
            else
            {
                break; // Sortir de la boucle si aucune transition n'est trouvée
            }
        }
        
    }


    /*public void AddState(State state)
    {
        states.Add(state.GetType(),state);
    }*/
    public void AddTransition(State state, StateT stateT)
    {
        state.AddTransition(stateT);
    }

    public void AddGlobalState(State state)
    {
        globalStates.Add(state);
    }
    /* public State CreateState(String name);
     {
         return new State(name);
     }*/
    public StateT CreateTransition(Func<bool> condition, State NextState)
    {
        return new StateT(condition, NextState);
    }
    public string getName()
    {
        return currentState.getName();
    }
}




