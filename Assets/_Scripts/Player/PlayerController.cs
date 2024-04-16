using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

[Serializable]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Min(0)] private Vector2 _checkGroundSize;
    [SerializeField] private Vector2 _checkGroundOffset;
    [SerializeField] private float _fallingGravityMultiplier;

    private Rigidbody2D _rb;
    private bool _isFalling = false;

    private Vector3 PositionWithOffset
    {
        get
        {
            var scaledOffset = Vector2.Scale(_checkGroundOffset, transform.localScale);
            var rotatedPosition = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * scaledOffset;
            return new Vector3(transform.position.x + rotatedPosition.x, transform.position.y + rotatedPosition.y);
        }
    }

    public bool IsGrounded { get; private set; } = false;
    public bool IsFalling => _isFalling;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        CheckGround();
    }

    public void Move(float direction, float speed)
    {
        _rb.velocity = new Vector2(direction * speed, _rb.velocity.y);
    }

    public void Jump(float force)
    {
        if (IsGrounded)
            _rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }

    public void Fall()
    {
        if (IsGrounded || _isFalling) return;
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(-transform.up * 10, ForceMode2D.Impulse);
        _isFalling = true;
    }

    private void CheckGround()
    {
        var scaledSize = Vector2.Scale(_checkGroundSize, transform.localScale);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(PositionWithOffset, scaledSize, transform.rotation.z);
        if (colliders.Length > 2) // player + camera border = 2
        {
            IsGrounded = true;
            if (_isFalling)
            {
                _isFalling = false;
            }
        }
        else IsGrounded = false;          
    }
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_checkGroundOffset, _checkGroundSize);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, Vector3.up);
    }
}