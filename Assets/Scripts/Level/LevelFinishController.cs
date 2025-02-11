using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelFinishController : MonoBehaviour
{
    private int woodCount;
    [Inject] private List<Wood> woods;
    [Inject] private SignalBus signalBus;

    private void Awake()
    {
        woodCount = woods.Count;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Wood>() != null)
        {
            woodCount--;
            if (woodCount <= 0)
            {
                signalBus.TryFire<LevelFinishedSignal>();
            }
        }
    }
}
