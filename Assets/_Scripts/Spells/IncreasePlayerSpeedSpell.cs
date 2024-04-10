using Zenject;

public class IncreasePlayerSpeedSpell : BaseSpell
{
    private Player _player;
    private PlayerSpeed _scale;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
        _scale = _player.GetComponent<PlayerSpeed>();
    }
    public override void DoMagic()
    {
        _scale.IncreaseSpeed();
    }
}
