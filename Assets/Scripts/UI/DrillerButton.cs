using UnityEngine;
using Zenject;

public class DrillerButton : MonoBehaviour
{
    [Inject] private MouseEvents mouseEvents;

    public void OnClick()
    {
        mouseEvents.StateMachine.ChangeState(mouseEvents.DrillerState);
    }
}
