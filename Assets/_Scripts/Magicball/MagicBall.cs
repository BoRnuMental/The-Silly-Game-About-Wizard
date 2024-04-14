using System;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public BaseSpell Spell { get; set; }

    private bool IsPlayer(Collider2D collision)
    {
        return collision.TryGetComponent(out Player player);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            Spell?.DoMagic();
            gameObject.SetActive(false);
        }      
    }
}