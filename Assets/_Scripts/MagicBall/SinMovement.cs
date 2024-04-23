using UnityEngine;

public class SinMovement : BaseMagicBallMovement
{
    private float _height = 1f;
    private float _width = 10f;
    private Vector2 _startPosition;
    private float _offset;

    public SinMovement() : base() { }
    public SinMovement(Transform magicBall, Vector2 direction, float speed) : base(magicBall, direction, speed)
    {
        _startPosition = magicBall.position;
        _height /= 2;
        _offset = Random.Range(0f, _width);
    }

    public override void Move()
    {
        Vector2 previousPosition = _magicBall.position;
        _magicBall.Translate(Speed * Time.fixedDeltaTime * _direction, Space.World);
        var x = _magicBall.position.x;
        var y = _startPosition.y;
        _magicBall.position = new Vector3(x, y + _height * Mathf.Sin((x - _offset) * Mathf.PI * (1 / _width)));
        _magicBall.right = (Vector2) _magicBall.position - previousPosition;
    }
}
