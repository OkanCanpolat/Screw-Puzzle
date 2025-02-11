using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class DrillerState : IMouseEventState
{
    [Inject(Id = "BoardHole")] private BoardHole BoardHolePrefab;
    [Inject] private Board board;
    [Inject] private MouseEvents mouseEvents;
    public void OnEnter()
    {
        board.Shine(true);
    }

    public void OnExit()
    {
        board.Shine(false);
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f);
            
            if (hit.collider == null || hit.collider.GetComponent<Board>() == null) return;

            Vector3 createPos = new Vector3(mousePos.x, mousePos.y, 0);

            GameObject.Instantiate(BoardHolePrefab, createPos, Quaternion.identity);

            mouseEvents.StateMachine.ChangeState(mouseEvents.UnselectedState);
        }
    }
}
