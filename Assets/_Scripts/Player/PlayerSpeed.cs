using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] private int _defaultLevel = 2;
    [SerializeField] private float[] _speedLevels;

    private int _currentLevel;
    private Player _player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _currentLevel = _defaultLevel;
        _player.Speed = _speedLevels[_defaultLevel];
    }
    public void IncreaseSpeed()
    {
        if (_currentLevel >= _speedLevels.Length - 1) return;
        _player.Speed = _speedLevels[++_currentLevel];
        CalculateAnimationSpeed();  
    }

    public void DecreaseSpeed()
    {
        if (_currentLevel <= 0) return;
        _player.Speed = _speedLevels[--_currentLevel];
        CalculateAnimationSpeed();
    }

    private void CalculateAnimationSpeed()
    {
        if (_speedLevels[_defaultLevel] == 0)
        {
            Debug.LogWarning("Division by zero");
            return;
        }
        var multiplier = _player.Speed / _speedLevels[_defaultLevel];
        _animator.SetFloat("SpeedMultiplier", multiplier);
    }
}
