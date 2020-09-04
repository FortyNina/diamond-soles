using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePool : GameObjectPool
{


    public GameObject PullActiveObjectOfTileType(TileType t)
    {

        for(int i = 0;i < _pooledObjs.Count; i++)
        {
            if (_pooledObjs[i].GetComponent<FloorTile>().tileType == t && _pooledObjs[i].Active)
                return _pooledObjs[i].gameObject;
        }
        return null;

    }






}
