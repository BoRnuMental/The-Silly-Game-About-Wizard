using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonoPool<T> where T : MonoBehaviour
{
    private List<T> _objects;
    private GameObject _prefab;
    private Transform _parent;
    private DiContainer _container;
    private bool _autoExpand;
    private readonly uint _count;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
        CreatePool();
    }


    public MonoPool(uint count, Transform parent, bool autoExpand = false)
    {
        _objects = new List<T>();
        _count = count;
        _parent = parent;
        _autoExpand = autoExpand;
    }

    private void CreatePool()
    {
        LoadPrefab();
        for (int i = 0; i < _count; i++)
        {
            T newObject = CreateObject();
            if (!newObject) return;
            _objects.Add(newObject);
        }
    }
    public bool TryGetFreeObject(out T result)
    {
        foreach(T obj in _objects)
        {
            if (!obj.gameObject.activeSelf)
            {
                result = obj;
                result.gameObject.SetActive(true);
                return true;
            }          
        }
        if (_autoExpand)
        {
            result = CreateObject();
            if (!result) return false;
            _objects.Add(result);
            return true;
        }
        Debug.Log($"Pool have no any free object of type {typeof(T)}");
        result = null;
        return false;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.localPosition = Vector3.zero;
    }
    private void LoadPrefab()
    {
        _prefab = Resources.Load<GameObject>($"Prefabs/{typeof(T)}");
        if (!_prefab) Debug.LogWarning($"Cant find prefab with name {typeof(T)}");
    }
    private T CreateObject()
    {
        var newObject = _container.InstantiatePrefabForComponent<T>(_prefab, _parent);
        newObject.gameObject.SetActive(false);
        return newObject;
    }
}
