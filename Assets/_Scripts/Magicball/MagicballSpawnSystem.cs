using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MagicBallSpawnSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private uint _startObjectCount;
    [SerializeField] private Transform _poolParent;

    private DiContainer _container;
    private MonoPool<MagicBall> _pool;
    private SpellsData _spellWeights;
    private DifficultySystem _difficultySystem;

    private float Frequency
    {
        get
        {
            if (_difficultySystem.CountPerMinute <= 0) return 0;
            return 60 / _difficultySystem.CountPerMinute;
        }
    }

    public BaseMagicBallMovement Movement { get; set; }

    [Inject]
    private void Construct(DiContainer container, SpellsData spellWeight, DifficultySystem difficultySystem)
    {
        _container = container;
        _pool = _container.Instantiate<MonoPool<MagicBall>>(new object[] { _startObjectCount, _poolParent, true });
        _spellWeights = spellWeight;
        _difficultySystem = difficultySystem;
    }

    private void Awake()
    {
        foreach (var spell in _spellWeights.Spells)
            _container.Inject(spell.Spell);
    }

    private void Spawn()
    {
        if (!_pool.TryGetFreeObject(out var magicBall))
        {
            Debug.LogWarning("No free object");
            return;
        }
        magicBall.Spell = GetRandomSpell();

        var mover = magicBall.GetComponent<MagicBallMover>();
        Transform randomPoint = _points[UnityEngine.Random.Range(0, _points.Count)];
        magicBall.transform.position = randomPoint.position;
        Vector2 direction = new Vector2(0f - randomPoint.position.x, 0f).normalized;
        var randomSpeed = UnityEngine.Random.Range(_difficultySystem.MagicBallSpeed - _difficultySystem.SpeedRange / 2,
            _difficultySystem.MagicBallSpeed + _difficultySystem.SpeedRange / 2);

        mover.Movement = GetMovement(magicBall.transform, direction, randomSpeed);

        var color = magicBall.GetComponent<MagicBallColor>();
        color.SetColor();
    }

    private BaseMagicBallMovement GetMovement(Transform magicBall, Vector2 direction, float randomSpeed)
    {
        return (Movement) switch
        {
            LinearMovement => new LinearMovement(magicBall, direction, randomSpeed),
            SinMovement => new SinMovement(magicBall, direction, randomSpeed),
            _ => new LinearMovement(magicBall, direction, randomSpeed)
        };
    }
    private BaseSpell GetRandomSpell()
    {
        int totalWeight = 0;
        foreach(var spellInfo in _spellWeights.Spells)
            totalWeight += spellInfo.Weight;
        int random = UnityEngine.Random.Range(1, totalWeight + 1);
        int sum = 0;
        foreach(var spellInfo in _spellWeights.Spells)
        {
            sum += spellInfo.Weight;
            if (sum >= random) return spellInfo.Spell;
        }
        return null;
    }
    private void OnEnable()
    {
        StartCoroutine(SpawnWithDelay());
    }
    private void OnDisable()
    {
        StopCoroutine(SpawnWithDelay());
    }
    private IEnumerator SpawnWithDelay()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(Frequency);
            if (Frequency != 0f) Spawn();
        }   
    }
}