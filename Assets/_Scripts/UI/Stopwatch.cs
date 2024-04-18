using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Zenject;
public class Stopwatch : MonoBehaviour
{
    private TMP_Text _text;
    private Vector3 _startPosition;
    private GameManager _gameManager;
    public float TimePassed { get; private set; } = 0f;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _startPosition = transform.localPosition;
    }

    private void Update()
    {
        TimePassed += Time.unscaledDeltaTime;
        var time = TimeSpan.FromSeconds(TimePassed);
        if (time.Hours > 0)
        {
            _text.text = $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds / 10:00}";
            return;
        }
        _text.text = $"{time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds / 10:00}";
    }

    public void Hide()
    {
        transform.DOLocalMove(_startPosition, _gameManager.ShowGameOverDelay).SetEase(Ease.InQuart);
    }
}
