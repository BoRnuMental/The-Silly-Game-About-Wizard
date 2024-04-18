using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Player"),SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;

    [Space,Tooltip("Time before the game starts")]
    [SerializeField] private PrepareTimer _timer;
    [SerializeField] private Stopwatch _stopwatch;
    [SerializeField] private SpellsData _spellsData;
    [SerializeField] private TierColors _tierColors;

    [Header("Systems"),SerializeField] private GameManager _gm;
    [SerializeField] private MagicBallSpawnSystem _spawnSystem;
    [SerializeField] private DifficultySystem _difficultySystem;
    [SerializeField] private BestTimeSystem _bestTimeSystem;
    [SerializeField] private SoundSystem _soundSystem;

    [Header("UI"), SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _settings;

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
        Container.Bind<MagicBallSpawnSystem>().FromInstance(_spawnSystem).AsSingle();
        Container.Bind<DifficultySystem>().FromInstance(_difficultySystem).AsSingle();
        Container.Bind<BestTimeSystem>().FromInstance(_bestTimeSystem).AsSingle();
        Container.Bind<SoundSystem>().FromInstance(_soundSystem).AsSingle();
    }

    private void BindUI()
    {
        Container.BindInstance(_timer).AsSingle();
        Container.BindInstance(_stopwatch).AsSingle();
        Container.BindInstance(_pauseMenu).WithId("PauseMenu");
        Container.BindInstance(_gameOverMenu).WithId("GameOverMenu");
        Container.BindInstance(_settings).WithId("Settings");
    }

    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<TimerEndedSignal>().OptionalSubscriber();
        Container.DeclareSignal<TimerHiddenSignal>().OptionalSubscriber();
        Container.DeclareSignal<PlayerSpawnedSignal>().OptionalSubscriber();
        Container.DeclareSignal<PlayerScaleOutOfRangeSignal>().OptionalSubscriber();
        Container.DeclareSignal<PlayerDiedSignal>().OptionalSubscriber();
        Container.DeclareSignal<PauseButtonPressed>().OptionalSubscriber();
    }

    private void BindSpells()
    {
        Container.Bind<SpellsData>().FromInstance(_spellsData).AsSingle();
        Container.Bind<TierColors>().FromInstance(_tierColors);
    }
}