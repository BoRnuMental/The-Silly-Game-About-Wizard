using UnityEngine;

public class PlayerHitSignal
{
    
    public BaseSpell spell;
    public Vector2 position;

    public PlayerHitSignal(BaseSpell spell, Vector2 position)
    {
        this.spell = spell;
        this.position = position;
    }
}
