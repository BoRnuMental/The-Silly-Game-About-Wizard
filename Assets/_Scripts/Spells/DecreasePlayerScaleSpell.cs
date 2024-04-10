using Zenject;

public class DecreasePlayerScaleSpell : BaseSpell
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
        _scale.DecreaseScale();
    }
}
