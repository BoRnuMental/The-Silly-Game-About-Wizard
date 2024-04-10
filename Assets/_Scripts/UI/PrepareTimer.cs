using UnityEngine;
using Zenject;
using TMPro;
using System;

public class PrepareTimer : MonoBehaviour
{
    private float _timer;
    private float _timeLeft;
    private SignalBus _signalBus;
    private TMP_Text _text;

    [Inject]
    private void Construct(GameManager gameManager, SignalBus signalBus)
    {
        _signalBus = signalBus;
        _timer = gameManager.PrepareTimer;
        _timeLeft = _timer;
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0)
        {
            _timeLeft = 0;
            _signalBus.TryFire<TimerEndedSignal>();
            enabled = false;
        }
        var time = TimeSpan.FromSeconds(_timeLeft);
        _text.text = $"{time.Seconds:00}:{time.Milliseconds / 10:00}";
    }

    private void OnEnable()
    {
        _timeLeft = _timer;
    }
}
