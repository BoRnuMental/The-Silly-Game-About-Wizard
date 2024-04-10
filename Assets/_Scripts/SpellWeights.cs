using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell Weights", menuName = "Scriptable objects/Create SpellWeights SO")]
public class SpellWeights : ScriptableObject
{
    [SerializeField] private List<SpellInfo> _spells;
    public List<SpellInfo> Spells => _spells;
}