using UnityEngine;
using Zenject;

public class GameOverSystem : MonoBehaviour
{
    private SignalBus _signalBus;
    private Player _player;
    private SoundSystem _soundSystem;

    [Inject] 
    private void Construct(SignalBus signalBus, Player player, SoundSystem soundSystem)
    {
        _signalBus = signalBus;
        _player = player;
        _soundSystem = soundSystem;
    }
    private void Awake()
    {
        _signalBus.Subscribe<PlayerScaleOutOfRangeSignal>(HandleSignal);
        _signalBus.Subscribe<PlayerDiedSignal>(KillPlayer);
    }
    private void OnDestroy()
    {
        _signalBus.Unsubscribe<PlayerScaleOutOfRangeSignal>(HandleSignal);
        _signalBus.Unsubscribe<PlayerDiedSignal>(KillPlayer);
    }

    private void PopPlayer()
    {
        _player.gameObject.SetActive(false);
        _signalBus.Fire<PlayerDiedSignal>();
        _soundSystem.PlaySound("GameplayPop"); 
        // particles
    }

    private void VanishPlayer()
    {
        _player.gameObject.SetActive(false);
        _signalBus.Fire<PlayerDiedSignal>();
        _soundSystem.PlaySound("GameplayVanish");
    }

    private void KillPlayer()
    {
        _player.gameObject.SetActive(false);
        //animation
    }

    private void HandleSignal(PlayerScaleOutOfRangeSignal args)
    {
        switch (args.Bound)
        {
            case Bound.Upper:
                PopPlayer();
                break;
            case Bound.Lower:
                VanishPlayer();
                break;
        }
    }
}
