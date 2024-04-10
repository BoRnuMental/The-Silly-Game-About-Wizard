using UnityEngine;

public class MagicballMover : MonoBehaviour
{
    private BaseMagicballMovement _movement;

    public BaseMagicballMovement Movement 
    { 
        get => _movement; 
        set => _movement = value ?? _movement; 
    }

    private void FixedUpdate()
    {
        _movement.Move();
    }
}
