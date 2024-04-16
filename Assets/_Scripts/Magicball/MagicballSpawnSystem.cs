using Cinemachine.Utility;
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
    private bool _autoExpandPool = true;

    private float Frequency
    {
        get
        {
            if (_difficultySystem.CountPerMinute <= 0) return 0;
            return 60 / _difficultySystem.CountPerMinute;
        }
    }

    [Inject]
    private void Construct(DiContainer container, SpellsData spellWeight, DifficultySystem difficultySystem)
    {
        _container = container;
        _pool = _container.Instantiate<MonoPool<MagicBall>>(new object[]{ _startObjectCount, _poolParent, _autoExpandPool });
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
        if(!_pool.TryGetFreeObject(out var magicBall))
        {
            Debug.Log("No free object");
            return;
        }
        magicBall.Spell = GetRandomSpell();
        var mover = magicBall.GetComponent<MagicBallMover>();
        Transform randomPoint = _points[Random.Range(0, _points.Count)];
        magicBall.transform.position = randomPoint.position;
        Vector2 direction = new(0f - randomPoint.position.x, 0f);
        mover.Movement = new LinearMovement(magicBall.transform, direction.normalized,
            Random.Range(_difficultySystem.MagicBallSpeed - _difficultySystem.SpeedRange / 2,
            _difficultySystem.MagicBallSpeed + _difficultySystem.SpeedRange / 2));
        FlipMagicBall(direction.normalized, magicBall.transform);
        var color = magicBall.GetComponent<MagicBallColor>();
        color.SetColor();
    }

    private BaseSpell GetRandomSpell()
    {
        int totalWeight = 0;
        foreach(var spellInfo in _spellWeights.Spells)
            totalWeight += spellInfo.Weight;
        int random = Random.Range(1, totalWeight + 1);
        int sum = 0;
        foreach(var spellInfo in _spellWeights.Spells)
        {
            sum += spellInfo.Weight;
            if (sum >= random) return spellInfo.Spell;
        }
        return null;
    }

    private void FlipMagicBall(Vector2 direction, Transform transform)
    {
        if (direction.x * transform.localScale.x < 0) 
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1f, 1f, 1f));
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
            Debug.Log(Frequency);
            if (Frequency != 0f) Spawn();
        }   
    }
}