using System;

public interface IStateTransition
{
    public bool Evaluate();
}
public class StateT : IStateTransition
{
    private Func<bool> condition;
    private IState nextState;

    public StateT(Func<bool> condition, State nextState)
    {
        this.condition = condition;
        this.nextState = nextState;
    }

    public bool Evaluate()
    {
        return condition();
    }

    public IState GetNextState()
    {
        return nextState;
    }
}