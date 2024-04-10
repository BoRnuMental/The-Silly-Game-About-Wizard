using UnityEngine;
using Zenject;

public class FlipGravitySpell : BaseSpell
{
    private Player _player;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
        _sr = _player.GetComponent<SpriteRenderer>();
        _rb = _player.GetComponent<Rigidbody2D>();
    }

    public override void DoMagic()
    {
        var rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0f, 0f, 180f);
        _player.transform.rotation *= rotation;
        _sr.flipY = true;
        _rb.gravityScale = -_rb.gravityScale;
    }
}
