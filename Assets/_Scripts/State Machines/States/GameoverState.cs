using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameoverState : GameState
{

    private MagicBallSpawnSystem _spawnSystem;
    private GameObject _gameOver;
    private float _delay;

    [Inject]
    private void Construct(
        [Inject(Id = "GameOverMenu")] GameObject gameOver,
        MagicBallSpawnSystem spawnSystem,   
        GameManager gameManager)
    {
        _spawnSystem = spawnSystem;
        _gameOver = gameOver;
        _delay = gameManager.ShowGameOverDelay;
    }
    public override void Enter()
    {
        _spawnSystem.gameObject.SetActive(true);
        _gameOver.SetActive(true);
    }

    public override void Exit()
    {
        _spawnSystem.gameObject.SetActive(false);
        _gameOver.SetActive(false);
    }
}
