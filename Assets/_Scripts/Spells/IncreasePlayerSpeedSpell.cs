using Zenject;

public class IncreasePlayerSpeedSpell : BaseSpell
{
    private Player _player;
    private PlayerSpeed _scale;
    private SoundSystem _soundSystem;

    [Inject]
    private void Construct(Player player, SoundSystem soundSystem)
    {
        _player = player;
        _scale = _player.GetComponent<PlayerSpeed>();
        _soundSystem = soundSystem;
    }
    public override void DoMagic()
    {
        _scale.IncreaseSpeed();
        _soundSystem.PlaySound("GameplayIncreaseSpeed");
    }
}
