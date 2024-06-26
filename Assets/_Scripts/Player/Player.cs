using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(PlayerController))]

public class Player : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField, Min(0f)] private float _jumpForce;

    private PlayerController _controller;
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private float _movementDirection;
    private bool _jumped = false;
    private bool _axisInUse = false;

    public float Speed 
    { 
        get => _speed;
        set
        {
            if (value < 0f) return;
            _speed = value;
        }
    }

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        ReadMovement();
        ReadJump();
        ReadFall();
        SetAnimations();
    }
    private void FixedUpdate()
    {
        _controller.Move(_movementDirection, Speed);
    }
    private void ReadJump()
    {
        if (Input.GetAxisRaw("Jump") > 0f && !_jumped)
        {
            if (_axisInUse) return;
            _controller.Jump(_jumpForce);
            _animator.SetTrigger("Jump");
            _jumped = true;
            _axisInUse = true;
        }
        if (!_controller.IsGrounded) _jumped = false;
        if (Input.GetAxisRaw("Jump") == 0f) _axisInUse = false;
    }
    private void ReadFall()
    {
        if (Input.GetAxisRaw("Vertical") < 0f)
        {
            _controller.Fall();
        }
    }
    private void ReadMovement()
    {
        _movementDirection = Input.GetAxisRaw("Horizontal");
    }

    private void SetAnimations()
    {
        _animator.SetBool("Falling", Vector2.Dot(new Vector2(0f, _rb.velocity.y).normalized, transform.up) < 0f);
        _animator.SetBool("IsGrounded", _controller.IsGrounded);
        _animator.SetBool("Running", _movementDirection != 0f);

        if (Mathf.Abs(_rb.velocity.x) < 0.01f) return;
        if (Vector2.Dot(transform.up, Vector2.up) > 0f)
            _sr.flipX = _rb.velocity.x < 0f;
        else
            _sr.flipX = _rb.velocity.x > 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.right);
    }
}