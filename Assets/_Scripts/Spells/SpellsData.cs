using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spells Data", menuName = "Scriptable objects/Create Spells Data")]
public class SpellsData : ScriptableObject
{
    [SerializeField] private List<SpellInfo> _spells;
    public List<SpellInfo> Spells => _spells;
}