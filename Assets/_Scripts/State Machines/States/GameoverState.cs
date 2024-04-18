using System.Collections;
using UnityEngine;
using Zenject;

public class GameOverState : GameState
{
    private DifficultySystem _difficultySystem;
    private MagicBallSpawnSystem _spawnSystem;
    private GameObject _gameOver;
    private Stopwatch _stopwatch;
    private float _delay;

    [Inject]
    private void Construct(
        [Inject(Id = "GameOverMenu")] GameObject gameOver,
        MagicBallSpawnSystem spawnSystem,   
        GameManager gameManager,
        DifficultySystem difficultySystem,
        Stopwatch stopwatch)
    {
        _difficultySystem = difficultySystem;
        _spawnSystem = spawnSystem;
        _gameOver = gameOver;
        _delay = gameManager.ShowGameOverDelay;
        _stopwatch = stopwatch;
    }
    public override void Enter()
    {
        Cursor.visible = true;
        _spawnSystem.gameObject.SetActive(true);
        _stopwatch.Hide();
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
