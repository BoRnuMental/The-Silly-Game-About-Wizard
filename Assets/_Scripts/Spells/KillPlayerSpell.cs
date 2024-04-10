using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class KillPlayerSpell : BaseSpell
{
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    public override void DoMagic()
    {
        _signalBus.Fire<PlayerDiedSignal>();
    }
}
