using Zenject;

public class SinMovementSpell : BaseSpell
{
    private MagicBallSpawnSystem _spawnSystem;
    private SoundSystem _soundSystem;

    [Inject]
    private void Construct(SoundSystem soundSystem, MagicBallSpawnSystem spawnSystem)
    {
        _soundSystem = soundSystem;
        _spawnSystem = spawnSystem;
    }
    public override void DoMagic()
    {
        _soundSystem.PlaySound("GameplayChangeMovement");
        if (_spawnSystem.Movement is SinMovement)
            _spawnSystem.Movement = new LinearMovement();
        else
            _spawnSystem.Movement = new SinMovement();
    }
}
