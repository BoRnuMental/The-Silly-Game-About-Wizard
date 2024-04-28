using UnityEngine;
using Zenject;

public class MagicBall : MonoBehaviour
{
    private SignalBus _signalBus;
    public BaseSpell Spell { get; set; }

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private bool IsPlayer(Collider2D collision)
    {
        return collision.TryGetComponent(out Player player);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsPlayer(collision)) return;
        Spell.DoMagic();
        gameObject.SetActive(false);
        _signalBus.Fire(new PlayerHitSignal(Spell, transform.position));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up);
    }
}