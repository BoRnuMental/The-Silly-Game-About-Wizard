using UnityEngine;
using Zenject;

public class PlayerScale : MonoBehaviour
{

    [SerializeField] private int _defaultLevel = 3;
    [SerializeField, Min(0f)] private float[] _scaleLevels = 
    {
        0.3f,
        0.5f,
        0.7f,
        1f,
        1.4f,
        1.6f,
        1.8f
    };

    private int _currentLevel;
    private Player _player;
    private SignalBus _signalBus;
    private SoundSystem _soundSystem;

    [Inject]
    private void Construct(SignalBus signalBus, SoundSystem soundSystem)
    {
        _signalBus = signalBus;
        _soundSystem = soundSystem;
    }


    private void Awake()
    {
        _player = GetComponent<Player>();
        _currentLevel = _defaultLevel;
        _player.transform.localScale = Vector2.one * _scaleLevels[_defaultLevel];
    }
    public void IncreaseScale()
    {
        if ( _currentLevel == _scaleLevels.Length - 1)
        {
            _signalBus.Fire(new PlayerScaleOutOfRangeSignal() { Bound = Bound.Upper});
            return;
        }
        _player.transform.localScale = Vector2.one * _scaleLevels[++_currentLevel];
        _soundSystem.PlaySound("GameplayIncreaseScale");
    }

    public void DecreaseScale()
    {
        if (_currentLevel == 0)
        {
            _signalBus.Fire(new PlayerScaleOutOfRangeSignal() { Bound = Bound.Lower });
            return;
        }
        _player.transform.localScale = Vector2.one * _scaleLevels[--_currentLevel];
        _soundSystem.PlaySound("GameplayDecreaseScale");
    }
}
