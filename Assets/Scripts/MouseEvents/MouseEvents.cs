using UnityEngine;
using Zenject;

public class MouseEvents : MonoBehaviour
{
    public LayerMask ScrewLayer;
    public LayerMask BoardHoleLayer;
    public LayerMask WoodHoleLayer;
    public LayerMask WoodLayer;
    public LayerMask BoardLayer;
    [HideInInspector] public Screw SelectedScrew;

    [Inject] public MouseEventStateMachine StateMachine;
    [Inject (Id = "UnSelected")] public MouseUnselectedState UnselectedState;
    [Inject] public ScrewSelectedState ScrewSelectedState;
    [Inject] public ScrewdriverState ScrewdriverState;
    [Inject] public DrillerState DrillerState;
    [Inject] public LockState LockState;
    [Inject] private SignalBus signalBus;

    private void Start()
    {
        signalBus.Subscribe<LevelFinishedSignal>(() => StateMachine.ChangeState(LockState));
    }
    private void Update()
    {
        StateMachine.CurrentState.OnUpdate();
    }
}
