using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerController))]

public class Player : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpForce;

    private PlayerController _controller;
    private GameInput _gameInput;
    private float _movementDirection;

    public float Speed 
    { 
        get => _speed;
        set
        {
            if (value < 0) return;
            _speed = value;
        }
    }

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _gameInput = new GameInput();
        _gameInput.Enable();
    }
    private void Update()
    {
        ReadMovement();
    }
    private void FixedUpdate()
    {
        _controller.Move(_movementDirection, Speed);
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _controller.Jump(_jumpForce);
    }
    private void ReadMovement()
    {
        _movementDirection = _gameInput.Gameplay.Movement.ReadValue<float>();
    }

    private void OnFallPerformed(InputAction.CallbackContext context)
    {
        _controller.Fall();
    }
    private void OnEnable()
    {
        _gameInput.Gameplay.Jump.performed += OnJumpPerformed;
        _gameInput.Gameplay.Fall.performed += OnFallPerformed;
    }
    private void OnDisable()
    {
        _gameInput.Gameplay.Jump.performed -= OnJumpPerformed;
        _gameInput.Gameplay.Fall.performed -= OnFallPerformed;
    }
}