using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    public int initialTime = 120;
    private int currentTime;
    private WaitForSeconds waitForSeconds;
    [Inject] private SignalBus signalBus;
    private Coroutine timerCoroutine;

    private void Awake()
    {
        waitForSeconds = new WaitForSeconds(1);
        currentTime = initialTime;
        signalBus.Subscribe<LevelFinishedSignal>(StopTimer);
    }

    private void Start()
    {
        timerCoroutine = StartCoroutine(StartTimer());
    }
    private IEnumerator StartTimer()
    {
        while (currentTime >= 0)
        {
            
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timerText.text = time.ToString(@"mm\:ss");

            if (currentTime <= 0)
            {
                signalBus.TryFire<LevelFailedSignal>();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                yield break;
            }

            yield return waitForSeconds;
            currentTime--;
        }
    }
    private void StopTimer()
    {
        StopCoroutine(timerCoroutine);
    }
}
