using Zenject;

public class DecreasePlayerSpeedSpell : BaseSpell
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
        _scale.DecreaseSpeed();
    }
}
