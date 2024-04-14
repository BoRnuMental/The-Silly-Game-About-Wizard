using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TierColors", menuName = "Scriptable objects/Create TierColors")]
public class TierColors : ScriptableObject
{
    [SerializeField] private TierColor[] _colors;
    public TierColor[] Colors => _colors;
}

[Serializable]
public class TierColor
{
    public MagicBallTier Tier;
    public Color Color;
}
