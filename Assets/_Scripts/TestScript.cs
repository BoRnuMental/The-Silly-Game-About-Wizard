using UnityEngine;
using Zenject;

public class TestScript : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;

    private SpellPopupSpawnSystem _spawnSystem;

    [Inject]
    private void Construct(SpellPopupSpawnSystem spawnSystem)
    {
        _spawnSystem = spawnSystem;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        }
    }
}
