using UnityEngine;
using Zenject;

public class GameOverSystem : MonoBehaviour
{
    private SignalBus _signalBus;

    [Inject] 
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
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
        Debug.Log("Pop");
    }

    private void VanishPlayer()
    {
        Debug.Log("Vanish");
    }

    private void KillPlayer()
    {
        Debug.Log("Kill");
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
