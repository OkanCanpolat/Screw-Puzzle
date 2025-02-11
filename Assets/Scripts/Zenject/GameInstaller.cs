using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public BoardHole BoardHolePrefab;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LevelFinishedSignal>().OptionalSubscriber();
        Container.DeclareSignal<LevelFailedSignal>().OptionalSubscriber();

        Container.Bind<Wood>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<Screw>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<MouseEvents>().FromComponentInHierarchy().AsSingle();

        Container.Bind<MouseEventStateMachine>().AsSingle();
        Container.Bind<ScrewdriverState>().AsSingle();
        Container.Bind<ScrewSelectedState>().AsSingle();
        Container.Bind<DrillerState>().AsSingle();
        Container.Bind<LockState>().AsSingle();
        Container.Bind(typeof(IMouseEventState), typeof(MouseUnselectedState)).WithId("UnSelected").To<MouseUnselectedState>().AsSingle();

        Container.Bind<Board>().FromComponentInHierarchy().AsSingle();

        Container.Bind<BoardHole>().WithId("BoardHole").FromInstance(BoardHolePrefab).AsSingle();
    }
}

public class LevelFinishedSignal { }
public class LevelFailedSignal { }