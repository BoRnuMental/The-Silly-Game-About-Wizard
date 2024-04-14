using System;
using UnityEngine;

public abstract class BaseMagicBallMovement
{
    protected Transform _magicBall;
    protected float _speed;
    protected Vector2 _direction;
    protected BaseMagicBallMovement(Transform magicBall, Vector2 direction, float speed)
    {
        _magicBall = magicBall;
        _direction = direction;
        _speed = speed;
    }
    public Vector2 Direction => _direction;
    public float Speed { get => _speed; set => Math.Abs(value); }
    abstract public void Move();
}
public enum Direction
{
    Left,
    Right,
    Up,
    Down,
}
