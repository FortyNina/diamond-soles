using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private bool _isActive;
    public bool Active
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    private int _index;
    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }

    private IPool _parentPool;
    public IPool ParentPool
    {
        get { return _parentPool; }
        set { ParentPool = value; }
    }


}
