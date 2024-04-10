using DG.Tweening;
using UnityEngine;
using Zenject;

public class TimerMover : MonoBehaviour
{
    [SerializeField, Min(0)] float _duration;
    [SerializeField, Min(0)] float _delay;

    private SignalBus _signalBus;
    private RectTransform _rt;
    private RectTransform _canvasRT;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _canvasRT = transform.parent.GetComponent<RectTransform>();
    }

    private void MoveTimer()
    {
        var scaler = Screen.height / 1080;
        DOTween.Sequence()
            .AppendInterval(_delay)
            .Append(transform.DOLocalMoveY(_canvasRT.rect.height/2 + _rt.rect.height, _duration).SetEase(Ease.InBack))
            .AppendCallback(() => _signalBus.TryFire<TimerHiddenSignal>());
        
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<TimerEndedSignal>(MoveTimer);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<TimerEndedSignal>(MoveTimer);
    }
}
