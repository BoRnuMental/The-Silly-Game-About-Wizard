using System;
using UnityEngine;

[Serializable]
public class SpellInfo
{
    [SerializeField] private BaseSpell _spell;
    [SerializeField, Min(0f)] private int _weight;
    public BaseSpell Spell => _spell;
    public int Weight => _weight;
    public int SpawnRate => _weight;
}