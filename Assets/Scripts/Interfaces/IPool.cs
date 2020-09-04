using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPool
{
    void CreatePool();
    void DestroyPool();
    void ExpandPool();

    List<T> FindPooledObjectsOfType<T>();
    List<T> FindActiveObjectsOfType<T>();
    List<T> FindInactiveObjectsOfType<T>();

    T PullActiveObjectOfType<T>();
    void ReturnObject(PooledObject o);
}
