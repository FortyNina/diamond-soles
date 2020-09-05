using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPool
{
    void CreatePool();
    void DestroyPool();
    void ExpandPool();

    T PullInactiveObjectOfType<T>();
    void ReturnObject(PooledObject o);
}
