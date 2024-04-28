using UnityEngine;
using Zenject;

public class ShowPause : MonoBehaviour
{
    SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _signalBus.Fire<PauseButtonPressedSignal>();
    }
}
