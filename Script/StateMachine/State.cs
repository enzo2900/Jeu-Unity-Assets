using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public interface IState
{
    public void Update();

    public IState NextState();
    public void OnInitialize();

    public void FixedUpdate();
    public void OnEnter();
    public string getName();
}



public abstract class State : IState
{
    
    private HashSet<StateT> transitions;

    private string name;
    public string getName()
    {
        return name;
    }
    public State(String name)
    {
        this.name = name;
        transitions = new HashSet<StateT>();
    }
    public abstract void Update();


    public virtual IState NextState()
    {
        foreach (var transition in transitions)
        {
            if (transition.Evaluate())
            {
                return transition.GetNextState();
            }
        }
        return null;

    }

    public void AddTransition(StateT stateT)
    {
        transitions.Add(stateT);
    }

    public abstract void OnEnter();

    public abstract void OnInitialize();

    public abstract void FixedUpdate();
    
}




