using System;
using UnityEngine;
using Zenject;

public class PrepareState : GameState
{
    private PrepareTimer _timer;

    [Inject] 
    private void Construct(PrepareTimer timer)
    {
        _timer = timer;
    }

    public override void Enter()
    {
        Debug.Log("Prepare +");
        _timer.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        Debug.Log("Prepare -");
        _timer.gameObject.SetActive(false);
    }
}
