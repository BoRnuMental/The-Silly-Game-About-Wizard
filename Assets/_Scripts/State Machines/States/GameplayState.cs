using System;
using UnityEngine;
using Zenject;

public class GameplayState : GameState
{
    private MagicBallSpawnSystem _spawnSystem;

    [Inject]
    private void Construct(MagicBallSpawnSystem spawnSystem)
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
        _spawnSystem.gameObject.SetActive(false);
        Debug.Log("Leave Gameplayer state");
    }
}
