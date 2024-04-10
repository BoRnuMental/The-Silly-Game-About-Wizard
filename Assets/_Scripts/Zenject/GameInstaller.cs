using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PrepareTimer _timer;
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gm;
    [SerializeField] private MagicballSpawnSystem _spawnSystem;
    [SerializeField] private SpellWeights _spellWeights;

    public override void InstallBindings()
    {      
        BindSystems();
        BindUI();
        DeclareSignals();
        BindSpells();
        BindPlayer();
    }

    private void BindPlayer()
    {
        var player = Container.InstantiatePrefabForComponent<Player>(_player, _spawnPoint.position, Quaternion.identity, null);
        Container.BindInstance(player).AsSingle();
    }

    private void BindSystems()
    {
        Container.Bind<GameManager>().FromInstance(_gm).AsSingle();
        Container.Bind<MagicballSpawnSystem>().FromInstance(_spawnSystem).AsSingle();
    }

    private void BindUI()
    {
        Container.BindInstance(_timer).AsSingle();
    }

    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<TimerEndedSignal>().OptionalSubscriber();
        Container.DeclareSignal<TimerHiddenSignal>().OptionalSubscriber();
        Container.DeclareSignal<PlayerSpawnedSignal>().OptionalSubscriber();
        Container.DeclareSignal<PlayerScaleOutOfRangeSignal>().OptionalSubscriber();
        Container.DeclareSignal<PlayerDiedSignal>().OptionalSubscriber();
    }

    private void BindSpells()
    {
        Dictionary<BaseSpell, int> spells = new();
        foreach (var spell in _spellWeights.Spells)
            spells.Add(spell.Spell, spell.Weight);
        Container.Bind<Dictionary<BaseSpell, int>>().FromInstance(spells).AsSingle();
    }
}