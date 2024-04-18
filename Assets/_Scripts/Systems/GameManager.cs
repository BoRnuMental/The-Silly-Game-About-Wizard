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
        Cursor.visible = false;
        QualitySettings.maxQueuedFrames = 2;
        _stateMachine = _container.Instantiate<GameStateMachine>();         
        _stateMachine.EnterIn<PrepareState>();

        _signalBus.Subscribe<TimerHiddenSignal>(OnPrepareTimerEnded);
        _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        _signalBus.Subscribe<PauseButtonPressed>(OnPause);
    }

    private void Update()
    {
        if (!Application.isFocused && _stateMachine.CurrentState is not PauseState)
        {
            OnPause();
        }
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
        _stateMachine.EnterIn<GameOverState>();
    }

    private void OnPause()
    {
        if (_stateMachine.CurrentState is GameOverState) return;
        else if (_stateMachine.CurrentState is PauseState)
        {
            switch (_lastState)
            {
                case GameplayState:
                    _stateMachine.EnterIn<GameplayState>();
                    break;
                case GameOverState:
                    _stateMachine.EnterIn<GameOverState>();
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