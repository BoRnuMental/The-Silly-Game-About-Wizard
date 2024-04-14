using UnityEngine;
using Zenject;

public class FlipGravitySpell : BaseSpell
{
    private Player _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
        _rb = _player.GetComponent<Rigidbody2D>();
        _sr = _player.GetComponent<SpriteRenderer>();
    }

    public override void DoMagic()
    {
        var rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0f, 0f, 180f);
        _player.transform.rotation *= rotation;
        _rb.gravityScale = -_rb.gravityScale;
        _sr.flipX = !_sr.flipX;

    }
}
