using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateMachine
{
    private DiContainer _container;
    public GameState CurrentState { get; private set; }

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        Initialize();
    }

    private readonly Dictionary<Type, GameState> _states = new()
    {
        [typeof(EntryState)] =  new EntryState(),
        [typeof(PrepareState)] = new PrepareState(),
        [typeof(GameplayState)] = new GameplayState(),
        [typeof(GameoverState)] = new GameoverState(),
        [typeof(PauseState)] = new PauseState(),
    };

    public void Initialize()
    {
        foreach (var state in _states.Values) _container.Inject(state);
        EnterIn<EntryState>();
    }
    
    public void EnterIn<T>() where T : GameState
    {
        CurrentState?.Exit();
        CurrentState = _states[typeof(T)];
        CurrentState.Enter();
    }
} 