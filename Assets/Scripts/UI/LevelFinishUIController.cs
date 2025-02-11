using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelFinishUIController : MonoBehaviour
{
    public GameObject WinScreen;
    public TMP_Text LevelName;
    [Inject] private SignalBus signalBus;

    private void Awake()
    {
        signalBus.Subscribe<LevelFinishedSignal>(OnLevelFinished);
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LevelName.text = levelIndex.ToString();
    }

    public void OnLevelFinished()
    {
        WinScreen.SetActive(true);
    }
}
