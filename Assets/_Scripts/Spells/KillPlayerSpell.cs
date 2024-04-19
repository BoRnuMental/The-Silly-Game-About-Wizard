using Zenject;

public class KillPlayerSpell : BaseSpell
{
    private SignalBus _signalBus;
    private SoundSystem _soundSystem;

    [Inject]
    private void Construct(SignalBus signalBus, SoundSystem soundSystem)
    {
        _signalBus = signalBus;
        _soundSystem = soundSystem;
    }
    public override void DoMagic()
    {
        _signalBus.Fire<PlayerDiedSignal>();
        _soundSystem.PlaySound("GameplayDeath");
    }
}
