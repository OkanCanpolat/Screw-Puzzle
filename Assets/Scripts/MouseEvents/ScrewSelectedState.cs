using UnityEngine;
using Zenject;

public class ScrewSelectedState : IMouseEventState
{
    [Inject] private MouseEvents mouseEvents;
   
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hitWood = Physics2D.Raycast(mousePos, Vector2.zero, 100f, mouseEvents.WoodLayer);
            RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector2.zero, 100f, mouseEvents.WoodHoleLayer);
            RaycastHit2D hit2 = Physics2D.Raycast(mousePos, Vector2.zero, 100f, mouseEvents.BoardHoleLayer);

            if (hitWood.collider != null && hit.Length <= 0 && hit2.collider != null) return;

            if (hit2.collider != null && mouseEvents.SelectedScrew != null)
            {
                BoardHole boardHole = hit2.collider.GetComponent<BoardHole>();

                if (!boardHole.IsEmpty) return;

                if (mouseEvents.SelectedScrew.ConnectedHole != boardHole)
                {
                    mouseEvents.SelectedScrew.transform.position = hit2.collider.transform.position;
                    mouseEvents.SelectedScrew.UnBind();
                    mouseEvents.SelectedScrew.ConnectedHole = boardHole;
                    mouseEvents.SelectedScrew.ConnectedHole.IsEmpty = false;
                    ControlWoodHit(hit);
                    mouseEvents.SelectedScrew.Close();
                    mouseEvents.StateMachine.ChangeState(mouseEvents.UnselectedState);
                }

                else
                {
                    mouseEvents.SelectedScrew.Close();
                    mouseEvents.SelectedScrew.ConnectedHole.IsEmpty = false;
                    mouseEvents.StateMachine.ChangeState(mouseEvents.UnselectedState);
                }
            }
        }
    }


    private void ControlWoodHit(RaycastHit2D[] hits)
    {
        if (hits.Length >= 1)
        {
            foreach (RaycastHit2D hit in hits)
            {
                WoodHole woodHole = hit.collider.GetComponent<WoodHole>();
                woodHole.Joint.enabled = true;
                woodHole.Joint.connectedBody = mouseEvents.SelectedScrew.rb2D;
                mouseEvents.SelectedScrew.Connections.Add(woodHole.Joint);
            }
        }
    }
}
