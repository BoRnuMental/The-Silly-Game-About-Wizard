using UnityEngine;
using Zenject;

public class GameOverSystem : MonoBehaviour
{
    private SignalBus _signalBus;
    private Player _player;

    [Inject] 
    private void Construct(SignalBus signalBus, Player player)
    {
        _signalBus = signalBus;
        _player = player;
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
        // audio
        // particles
    }

    private void VanishPlayer()
    {
        _player.gameObject.SetActive(false);
        _signalBus.Fire<PlayerDiedSignal>();

        //audio
    }

    private void KillPlayer()
    {
        _player.gameObject.SetActive(false);
        //animation
        //audio
        //particles
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
