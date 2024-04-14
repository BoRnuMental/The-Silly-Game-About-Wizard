using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MagicBallSpawnSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [Header("Difficulty")]
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField, Min(0f)] private float _spawnCountPerMinute;
    [Space]
    [SerializeField] private uint _startObjectCount; 
    [SerializeField] private Transform _poolParent;
   
    private DiContainer _container;
    private MonoPool<MagicBall> _pool;
    private SpellsData _spellWeights;
    private bool _autoExpandPool = true;
    private float _frequency;

    [Inject]
    private void Construct(DiContainer container, SpellsData spellWeight)
    {
        _container = container;
        _pool = _container.Instantiate<MonoPool<MagicBall>>(new object[]{ _startObjectCount, _poolParent, _autoExpandPool });
        _spellWeights = spellWeight;
    }

    private void Awake()
    {
        if (_spawnCountPerMinute != 0)
            _frequency = 60 / _spawnCountPerMinute;
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
        mover.Movement = new LinearMovement(magicBall.transform, direction.normalized, _speed);
        FlipMagicBall(direction.normalized, magicBall.transform);
        var color = magicBall.GetComponent<MagicBallColor>();
        color.SetColor();
    }

    private BaseSpell GetRandomSpell()
    {
        int totalWeigth = 0;
        foreach(var spellInfo in _spellWeights.Spells)
            totalWeigth += spellInfo.Weight;
        int random = Random.Range(1, totalWeigth + 1);
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
        while (true)
        {
            yield return new WaitForSeconds(_frequency);
            if (_frequency != 0f) Spawn();
        }   
    }
}