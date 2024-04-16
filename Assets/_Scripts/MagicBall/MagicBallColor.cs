using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

public class MagicBallColor : MonoBehaviour
{
    private Dictionary<BaseSpell, Color> _colors;
    private MagicBall _magicBall;
    private SpriteRenderer _sr;
    private Light2D _light;
    private Dictionary<MagicBallTier, Color> _tierColorPairs;

    [Inject]
    private void Construct(SpellsData spellsData, TierColors tierColors)
    {
        _colors = new Dictionary<BaseSpell, Color>();
        _tierColorPairs = new();
        foreach (var tierColor in tierColors.Colors)
        {
            _tierColorPairs.Add(tierColor.Tier, tierColor.Color);
        }
        foreach(var spell in spellsData.Spells)
        {
            _colors.Add(spell.Spell, _tierColorPairs[spell.Tier]);
        }
    }    
    private void Awake()
    {
        _magicBall = GetComponent<MagicBall>();
        _sr = GetComponent<SpriteRenderer>();
        _light = GetComponentInChildren<Light2D>();


    }
    public void SetColor()
    {
        if (_magicBall.Spell == null) return;
        _sr.color = _colors[_magicBall.Spell];
/*        if (_sr.color == _tierColorPairs[MagicBallTier.Deadly])
        {
            _light.color = new Color(0.41f, 0f, 0.77f, 1f);
            return;
        }*/
        _light.color = _sr.color;
    }
}