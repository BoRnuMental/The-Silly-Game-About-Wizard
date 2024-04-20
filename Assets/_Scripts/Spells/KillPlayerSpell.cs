using UnityEngine;
using Zenject;

public class KillPlayerSpell : BaseSpell
{
    private SignalBus _signalBus;
    private SoundSystem _soundSystem;
    private ParticleSystem _particleSystem;

    [Inject]
    private void Construct(SignalBus signalBus, SoundSystem soundSystem, Player player)
    {
        _signalBus = signalBus;
        _soundSystem = soundSystem;
        _particleSystem = player.GetComponentInChildren<ParticleSystem>();
    }
    public override void DoMagic()
    {
        _particleSystem.gameObject.transform.parent = null;
        _particleSystem.Play();
        _signalBus.Fire<PlayerDiedSignal>();
        _soundSystem.PlaySound("GameplayDeath");
    }
}
