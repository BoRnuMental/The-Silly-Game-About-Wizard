using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameOverState : GameState
{

    private MagicBallSpawnSystem _spawnSystem;
    private GameObject _gameOver;
    private DifficultySystem _difficultySystem;
    private float _delay;

    [Inject]
    private void Construct(
        [Inject(Id = "GameOverMenu")] GameObject gameOver,
        MagicBallSpawnSystem spawnSystem,   
        GameManager gameManager,
        DifficultySystem difficultySystem)
    {
        _difficultySystem = difficultySystem;
        _spawnSystem = spawnSystem;
        _gameOver = gameOver;
        _delay = gameManager.ShowGameOverDelay;
    }
    public override void Enter()
    {
        _spawnSystem.gameObject.SetActive(true);
        _difficultySystem.gameObject.SetActive(false);
        _spawnSystem.StartCoroutine(WaitForSeconds());
    }

    public override void Exit()
    {
        _spawnSystem.gameObject.SetActive(false);
        _gameOver.SetActive(false);
    }

    private IEnumerator WaitForSeconds()
    {
        yield return new WaitForSecondsRealtime(_delay);
        _gameOver.SetActive(true);
    }
}
