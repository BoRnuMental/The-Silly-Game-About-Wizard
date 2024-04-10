using System;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    private BaseSpell _spell;

    public void SetSpell(BaseSpell spell) 
    {
        if (spell == null) throw new ArgumentNullException(spell.ToString());
        _spell = spell;
    }

    private bool IsPlayer(Collider2D collision)
    {
        return collision.TryGetComponent(out Player player);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            _spell?.DoMagic();
            gameObject.SetActive(false);
        }      
    }
}