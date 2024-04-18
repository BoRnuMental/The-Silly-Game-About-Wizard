using UnityEngine;
using Zenject;

public class GameplayState : GameState
{
    private MagicBallSpawnSystem _spawnSystem;
    private Stopwatch _stopwatch;

    [Inject]
    private void Construct(MagicBallSpawnSystem spawnSystem, Stopwatch stopwatch)
    {
        _spawnSystem = spawnSystem;
        _stopwatch = stopwatch;
    }
    public override void Enter()
    {
        Debug.Log("Enter in Gameplay state");
        _spawnSystem.gameObject.SetActive(true);
        _stopwatch.gameObject.SetActive(true);
        _stopwatch.enabled = true;
    }

    public override void Exit()
    {
        _spawnSystem.gameObject.SetActive(false);
        _stopwatch.enabled = false;
        Debug.Log("Leave Gameplayer state");
    }
}
