using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScrewdriverButton : MonoBehaviour
{
    [Inject] private MouseEvents mouseEvents;

    public void OnClick()
    {
        mouseEvents.StateMachine.ChangeState(mouseEvents.ScrewdriverState);
    }
}
