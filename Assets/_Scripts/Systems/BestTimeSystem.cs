using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class BestTimeSystem : MonoBehaviour
{
    [SerializeField] private GameObject _newRecord;
    [SerializeField] private TMP_Text _yourTime;

    private SignalBus _signalBus;
    private Stopwatch _stopwatch;
    public float BestTime { get; private set; } = 0f;
    public TMP_Text YourTime => _yourTime;
    public GameObject NewRecord => _newRecord;

    [Inject]
    private void Construct(SignalBus signalBus, Stopwatch stopwatch)
    {
        _signalBus = signalBus;
        _stopwatch = stopwatch;
    }
    private void Awake()
    {
        _newRecord.SetActive(false);
    }
    private void OnEnable()
    {
        BestTime = SaveLoadSystem.LoadBestTime();
        _signalBus.Subscribe<PlayerDiedSignal>(SaveBestTime);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PlayerDiedSignal>(SaveBestTime);
    }

    private void SaveBestTime()
    {
        if (_stopwatch.TimePassed > BestTime)
        {
            SaveLoadSystem.SaveBestTime(_stopwatch.TimePassed);
            _newRecord.SetActive(true);
        }
        var timePassed = TimeSpan.FromSeconds(_stopwatch.TimePassed);
        if (timePassed.Hours > 0)
        {
            _yourTime.text += $"{timePassed.Hours:00}:{timePassed.Minutes:00}:{timePassed.Seconds:00}:{timePassed.Milliseconds / 10:00}";
            return;
        }
        _yourTime.text += $"{timePassed.Minutes:00}:{timePassed.Seconds:00}:{timePassed.Milliseconds / 10:00}";
    }
}
