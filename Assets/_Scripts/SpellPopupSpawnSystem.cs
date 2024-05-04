using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using Zenject;

public class SpellPopupSpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;

    private Dictionary<BaseSpell, string> _keys = new Dictionary<BaseSpell, string>();
    private SignalBus _signalBus;
    private SpellsData _spellsData;
    private Pool<RectTransform> _pool;

    [Inject]
    private void Construct(SignalBus signalBus, SpellsData spellsData)
    {
        _signalBus = signalBus;
        _spellsData = spellsData;
    }
    public void SpawnPopup(PlayerHitSignal playerHit)
    {
        _pool.TryGetFreeObject(out var popup);
        popup.position = playerHit.position;
        var local = popup.GetComponentInChildren<LocalizeStringEvent>();
        local.StringReference.SetReference("SpellDescriptions", _keys[playerHit.spell]);
        /*var popup = Instantiate(_prefab, playerHit.position, Quaternion.identity, _parent);
        var local = popup.GetComponentInChildren<LocalizeStringEvent>();
        local.StringReference.SetReference("SpellDescriptions", _keys[playerHit.spell]);*/
    }
    private void CheckOverlaps(RectTransform rt)
    {
        foreach(var popup in _pool.Objects)
        {
            if (popup == rt) continue;
            if (!popup.gameObject.activeSelf) continue;
            if (rt.Overlaps(popup)) 
                popup.gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        _pool = new Pool<RectTransform>(_prefab, 10, _parent);
        foreach (var spell in _spellsData.Spells)
            _keys.Add(spell.Spell, spell.LocalizationKey);
        _signalBus.Subscribe<PlayerHitSignal>(SpawnPopup);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<PlayerHitSignal>(SpawnPopup);   
    }
}