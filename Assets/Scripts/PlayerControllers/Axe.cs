using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public MinerResourceManager mrm;

    private int _playerID;
    public int PlayerID
    {
        get { return _playerID; }
        set { _playerID = value;  }
    }


}
