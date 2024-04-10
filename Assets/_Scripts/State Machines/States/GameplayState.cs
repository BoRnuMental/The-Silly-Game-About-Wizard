using System;
using UnityEngine;
using Zenject;

public class GameplayState : GameState
{
    private MagicballSpawnSystem _spawnSystem;

    [Inject]
    private void Construct(MagicballSpawnSystem spawnSystem)
    {
        _spawnSystem = spawnSystem;
    }
    public override void Enter()
    {
        Debug.Log("Enter in Gameplay state");
        _spawnSystem.gameObject.SetActive(true);
    }

    public override void Exit()
    {

    }
}
