using UnityEngine;

public class LinearMovement : BaseMagicballMovement
{
    public LinearMovement(Transform magicBall, Vector2 direction, float speed) : base(magicBall, direction, speed){}
    public override void Move()
    {
        _magicBall.Translate(_speed * Time.fixedDeltaTime * (Vector3)_direction);
    }
}
