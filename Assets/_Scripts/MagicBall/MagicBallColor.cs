using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MagicBallColor : MonoBehaviour
{
    private Dictionary<BaseSpell, Color> _colors;
    private MagicBall _magicBall;
    private SpriteRenderer _sr;

    [Inject]
    private void Construct(SpellsData spellsData, TierColors tierColors)
    {
        _colors = new Dictionary<BaseSpell, Color>();
        Dictionary<MagicBallTier, Color> tierColorPairs = new();
        foreach (var tierColor in tierColors.Colors)
        {
            tierColorPairs.Add(tierColor.Tier, tierColor.Color);
        }
        foreach(var spell in spellsData.Spells)
        {
            _colors.Add(spell.Spell, tierColorPairs[spell.Tier]);
        }
    }    
    private void Awake()
    {
        _magicBall = GetComponent<MagicBall>();
        _sr = GetComponent<SpriteRenderer>();
    }
    public void SetColor()
    {
        if (_magicBall.Spell == null) return;
        _sr.color = _colors[_magicBall.Spell];
    }
}