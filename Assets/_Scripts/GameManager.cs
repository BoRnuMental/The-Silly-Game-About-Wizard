using UnityEngine;
using Zenject;

// Entity that controls the game using state machine
public class GameManager : MonoBehaviour
{
    [SerializeField, Min(0), Tooltip("Time before the game starts in seconds")] 
    private float _prepareTimer; 
    public float PrepareTimer => _prepareTimer;

    private DiContainer _container;
    private GameStateMachine _stateMachine;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(DiContainer container, SignalBus signalBus)
    {
        _container = container;
        _signalBus = signalBus;
    }
    private void Awake()
    {
        _stateMachine = _container.Instantiate<GameStateMachine>();         
        _stateMachine.EnterIn<PrepareState>();

        _signalBus.Subscribe<TimerHiddenSignal>(OnPrepareTimerEnded);
    }
    private void OnDestroy()
    {
        _signalBus.Unsubscribe<TimerHiddenSignal>(OnPrepareTimerEnded);
    }
    private void OnPrepareTimerEnded()
    {
        _stateMachine.EnterIn<GameplayState>();
    }
}