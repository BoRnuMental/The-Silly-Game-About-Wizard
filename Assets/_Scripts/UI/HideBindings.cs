using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class HideBindings : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _duration;

    private SignalBus _signalBus;
    private TMP_Text _text;
    
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    private void Hide()
    {
        _text.DOFade(0f, _duration);
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<TimerHiddenSignal>(Hide);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<TimerHiddenSignal>(Hide);
    }
}
