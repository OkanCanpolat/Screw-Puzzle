using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScrewdriverState : IMouseEventState
{
    [Inject] private MouseEvents mouseEvents;
    [Inject] private List<Screw> screws;
   
    public void OnEnter()
    {
        foreach(Screw screw in screws)
        {
            screw.Shine(true);
        }
    }

    public void OnExit()
    {
        foreach (Screw screw in screws)
        {
            screw.Shine(false);
        }
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, mouseEvents.ScrewLayer);

            if (hit.collider == null) return;

            Screw screw = hit.collider.gameObject.GetComponent<Screw>();

            if (screw.Locked) return;

            screw.UnBind();
            screws.Remove(screw);
            screw.ConnectedHole.IsEmpty = true;
            GameObject.Destroy(screw.gameObject);
            mouseEvents.StateMachine.ChangeState(mouseEvents.UnselectedState);
        }
    }
}
