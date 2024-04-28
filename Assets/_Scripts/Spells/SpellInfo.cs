using System;
using UnityEngine;

[Serializable]
public class SpellInfo
{
    [SerializeField] private BaseSpell _spell;
    [SerializeField, Min(0f)] private int _weight;
    [SerializeField] private MagicBallTier _tier;
    [SerializeField] private string _localizationKey;
    public BaseSpell Spell => _spell;
    public int Weight => _weight;
    public MagicBallTier Tier => _tier;
    public string LocalizationKey => _localizationKey;
}

public enum MagicBallTier
{
    Harmless,
    LowDanger,
    MediumDanger,
    HighDanger,
    Deadly
}