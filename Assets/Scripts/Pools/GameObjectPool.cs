using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour, IPool
{
    [SerializeField] protected int[] _spawnNum;
    [SerializeField] protected GameObject[] _spawnObjs;
    protected List<PooledObject> _pooledObjs = new List<PooledObject>();

    private void OnEnable()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        Debug.Log("Creating a GameObject Pool with Pool Parent " + gameObject.name);
        for(int i = 0; i < _spawnObjs.Length; i++)
        {
            for (int j = 0; j < _spawnNum[i]; j++)
            {
                GameObject go = Instantiate(_spawnObjs[i], Vector3.zero, Quaternion.identity);
                if (go.GetComponent<PooledObject>() == null)
                {
                    Destroy(go);
                    Debug.LogError("An object you are trying to pool does not have a PooledObject component attached. These objects will not be added to the pool.");
                    continue;
                }
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;

                _pooledObjs.Add(go.GetComponent<PooledObject>());
            }
        }
    }



    #region Operations

    public void DestroyPool()
    {
        for(int i = 0; i < _pooledObjs.Count; i++)
        {
            Destroy(_pooledObjs[i]);
        }
        _pooledObjs.Clear();
    }

    public void ExpandPool()
    {
    }

    public List<T> FindPooledObjectsOfType<T>()
    {
        List<T> objs = new List<T>();
        for(int i = 0; i < _pooledObjs.Count; i++)
        {
            if (_pooledObjs[i].GetComponent<T>() != null)
                objs.Add(_pooledObjs[i].GetComponent<T>());
        }
        
        return objs;
    }

    public List<T> FindInactiveObjectsOfType<T>()
    {
        List<T> objs = new List<T>();
        for (int i = 0; i < _pooledObjs.Count; i++)
        {
            if (_pooledObjs[i].GetComponent<T>() != null && !_pooledObjs[i].Active)
                objs.Add(_pooledObjs[i].GetComponent<T>());
        }
        return objs;
    }

    public List<T> FindActiveObjectsOfType<T>()
    {
        List<T> objs = new List<T>();
        for (int i = 0; i < _pooledObjs.Count; i++)
        {
            if (_pooledObjs[i].GetComponent<T>() != null && _pooledObjs[i].Active)
                objs.Add(_pooledObjs[i].GetComponent<T>());
        }
        return objs;
    }

    public T PullActiveObjectOfType<T>()
    {
        for(int i = 0; i < _pooledObjs.Count; i++)
        {
            if (_pooledObjs[i].GetComponent<T>() != null && _pooledObjs[i].Active)
                return _pooledObjs[i].GetComponent<T>();
        }
        return default(T);
    }

    public void ReturnObject(PooledObject o)
    {
        o.Active = false;
        o.transform.parent = transform.parent;
    }


    #endregion
}
