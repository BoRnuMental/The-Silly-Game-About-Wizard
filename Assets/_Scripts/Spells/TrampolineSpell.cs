using UnityEngine;
using Zenject;

public class TrampolineSpell : BaseSpell
{
    private const float _pushForce = 13f;
    private Rigidbody2D _rb;
    private PlayerController _controller;
    private SoundSystem _soundSystem;
    private Player _player;

    [Inject]
    private void Construct(Player player, SoundSystem soundSystem)
    {
        _soundSystem = soundSystem;
        _player = player;
        _rb = player.GetComponent<Rigidbody2D>();
        _controller = player.GetComponent<PlayerController>();
    }

    public override void DoMagic()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(_player.transform.up * _pushForce, ForceMode2D.Impulse);
        _controller.IsFalling = false;
        _soundSystem.PlaySound("GameplayTrampoline");
    }
}