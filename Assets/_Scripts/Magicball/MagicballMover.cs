using UnityEngine;

public class MagicBallMover : MonoBehaviour
{
    private BaseMagicBallMovement _movement;

    public BaseMagicBallMovement Movement 
    { 
        get => _movement; 
        set => _movement = value ?? _movement; 
    }

    private void FixedUpdate()
    {
        _movement.Move();
    }
}