using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Component
{
    private List<T> _objects;
    private GameObject _prefab;
    private Transform _parent;
    private bool _autoExpand;
    private readonly uint _count;

    public List<T> Objects => _objects;

    public Pool(GameObject prefab, uint count, Transform parent, bool autoExpand = true)
    {
        _prefab = prefab;
        _parent = parent;
        _autoExpand = autoExpand;
        _count = count;
        _objects = new List<T>();
        CreatePool();
    }
    
    private void CreatePool()
    {
        for (int i = 0; i < _count; i++)
        {
            T newObject = CreateObject();
            newObject.gameObject.SetActive(false);
            _objects.Add(newObject);
        }
    }

    private T CreateObject()
    {
        var prefab = Object.Instantiate(_prefab, _parent.position, Quaternion.identity, _parent);
        T component = prefab.GetComponent<T>();
        return component;
    }

    public bool TryGetFreeObject(out T result)
    {
        foreach (T obj in _objects)
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

            result.gameObject.SetActive(true);
            return true;
        }
        Debug.Log($"Pool have no any free object of type {typeof(T)}");
        result = null;
        return false;
    }
}
