using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePool : GameObjectPool
{

    public static TilePool Instance;
    [SerializeField] private TileType[] _spawnTileTypes;

    public new void OnEnable()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if(_spawnTileTypes.Length != _spawnObjs.Length)
        {
            Debug.LogWarning("Your TilePool requires arrays SpawnTileTypes and SpawnObjects to be the same length. Different lengths can cause errors!");
        }
    }

    public GameObject PullInactiveObjectOfTileType(TileType t)
    {

        for(int i = 0;i < _pooledObjs.Count; i++)
        {
            if (_pooledObjs[i].GetComponent<FloorTile>().tileType == t && _pooledObjs[i].Active)
                return _pooledObjs[i].gameObject;
        }

        PooledObject p = CreateNewPooledObjectOfTileType(t);
        AddPooledObj(p);
        return p.gameObject;
    }


    private PooledObject CreateNewPooledObjectOfTileType(TileType t)
    {
        for(int i = 0; i < _spawnTileTypes.Length; i++)
        {
            if(_spawnTileTypes[i] == t)
            {
                GameObject go = Instantiate(_spawnObjs[i], Vector3.zero, Quaternion.identity);
                PooledObject pooled = go.GetComponent<PooledObject>();
                if (pooled == null)
                {
                    Destroy(go);
                    Debug.LogError("An object you are trying to pool does not have a PooledObject component attached. These objects will not be added to the pool.");
                    continue;
                }
                return pooled;
            }
        }
        return null;
    }


}
