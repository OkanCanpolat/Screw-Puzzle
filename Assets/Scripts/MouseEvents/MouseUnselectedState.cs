using UnityEngine;
using Zenject;

public class MouseUnselectedState : IMouseEventState
{
    [Inject] private MouseEvents mouseEvents;
    
    public void OnEnter()
    {
        mouseEvents.SelectedScrew = null;
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, mouseEvents.ScrewLayer);
            if (hit.collider != null)
            {
                Screw screw = hit.collider.gameObject.GetComponent<Screw>();

                if (screw.Locked) return;

                mouseEvents.SelectedScrew = screw;
                mouseEvents.SelectedScrew.Open();
                mouseEvents.SelectedScrew.ConnectedHole.IsEmpty = true;
                mouseEvents.StateMachine.ChangeState(mouseEvents.ScrewSelectedState);
            }
        }
    }
   
}
