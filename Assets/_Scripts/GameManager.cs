using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

// Entity that controls the game using state machine
public class GameManager : MonoBehaviour
{
    [SerializeField, Min(0f), Tooltip("Time before the game starts in seconds")] 
    private float _prepareTime;
    [SerializeField, Min(0f), Tooltip("Time before the game over menu is shown")]
    private float _showGameOverDelay;

    private DiContainer _container;
    private GameStateMachine _stateMachine;
    private SignalBus _signalBus;
    private GameState _lastState;
    

    public float PrepareTime => _prepareTime;

    public float ShowGameOverDelay => _showGameOverDelay;

    [Inject]
    private void Construct(DiContainer container, SignalBus signalBus)
    {
        _container = container;
        _signalBus = signalBus;
    }
    private void Awake()
    {
        QualitySettings.maxQueuedFrames = 2;
        _stateMachine = _container.Instantiate<GameStateMachine>();         
        _stateMachine.EnterIn<PrepareState>();

        _signalBus.Subscribe<TimerHiddenSignal>(OnPrepareTimerEnded);
        _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        _signalBus.Subscribe<PauseButtonPressed>(OnPause);
    }
    private void OnDestroy()
    {
        _signalBus.Unsubscribe<TimerHiddenSignal>(OnPrepareTimerEnded);
        _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
        _signalBus.Unsubscribe<PauseButtonPressed>(OnPause);
    }
    private void OnPrepareTimerEnded()
    {
        _stateMachine.EnterIn<GameplayState>();
    }

    private void OnPlayerDied()
    {
        _stateMachine.EnterIn<GameoverState>();
    }

    private void OnPause()
    {
        if (_stateMachine.CurrentState is GameoverState) return;
        else if (_stateMachine.CurrentState is PauseState)
        {
            switch (_lastState)
            {
                case GameplayState:
                    _stateMachine.EnterIn<GameplayState>();
                    break;
                case GameoverState:
                    _stateMachine.EnterIn<GameoverState>();
                    break;
                case PrepareState:
                    _stateMachine.EnterIn<PrepareState>();
                    break;
            }
        } // no OCP here
        else
        {
            _lastState = _stateMachine.CurrentState;
            _stateMachine.EnterIn<PauseState>();
        }
                  
    }
}