using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The foundation for a pool of GameObjects. Instantiates a set of GameObjects
/// when enabled that can be pooled and used by other objects in the scene.
/// </summary>
public abstract class GameObjectPool : MonoBehaviour, IPool
{
    [SerializeField] protected int[] _spawnNum;
    [SerializeField] protected GameObject[] _spawnObjs;

    //keeps a reference to all pooledObjects in the scene (that are a part of this pool)
    //DO NOT USE FOR LOOKUP OR POOLING. Should only be used for destroying the entire pool.
    protected List<PooledObject> _pooledObjs = new List<PooledObject>();

    protected void OnEnable()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        Debug.Log("Creating a GameObject Pool with Pool Parent " + gameObject.name);
        for(int i = 0; i < _spawnObjs.Length; i++)
        {
            Debug.Log(_spawnObjs[i].GetType());
            for (int j = 0; j < _spawnNum[i]; j++)
            {
                GameObject go = Instantiate(_spawnObjs[i], Vector3.zero, Quaternion.identity);
                PooledObject pooled = go.GetComponent<PooledObject>();
                if (pooled == null)
                {
                    Destroy(go);
                    Debug.LogError("An object you are trying to pool does not have a PooledObject component attached. These objects will not be added to the pool.");
                    continue;
                }
                AddPooledObj(pooled);
                
            }
        }
    }




    #region Operations

    protected virtual void AddPooledObj(PooledObject obj)
    {
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        _pooledObjs.Add(obj);
    }

    /// <summary>
    /// Destroy all objects in the pool, active and inactive.
    /// Objects are destroyed regardless of whether or not they are pooled.
    /// Make sure other scripts check for null objects!
    /// </summary>
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
        //TODO: implement? Not sure we need this right now 
    }

    public virtual T PullInactiveObjectOfType<T>()
    {
        for(int i = 0; i < _pooledObjs.Count; i++)
        {
            if (_pooledObjs[i].GetComponent<T>() != null && _pooledObjs[i].Active)
                return _pooledObjs[i].GetComponent<T>();
        }
        return default(T);
    }

    /// <summary>
    /// Returns a given gameobject with attached PooledObject component o to the pool.
    /// </summary>
    /// <param name="o"></param>
    public void ReturnObject(PooledObject o)
    {
        o.Active = false;
        o.transform.parent = transform.parent;
        o.transform.localPosition = Vector3.zero;
    }

    

    #endregion
}
