using Zenject;

public class IncreasePlayerScaleSpell : BaseSpell
{
    private Player _player;
    private PlayerScale _scale;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
        _scale = _player.GetComponent<PlayerScale>();
    }
    public override void DoMagic()
    {
        _scale.IncreaseScale();
    }
}
