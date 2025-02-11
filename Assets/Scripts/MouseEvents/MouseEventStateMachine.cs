using UnityEngine;
using Zenject;

public class MouseEventStateMachine 
{
    public IMouseEventState CurrentState;
    public MouseEventStateMachine([Inject (Id = "UnSelected")]IMouseEventState initialState)
    {
        CurrentState = initialState;
        CurrentState.OnEnter();
    }
    public void ChangeState(IMouseEventState state)
    {
        CurrentState.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }
}
