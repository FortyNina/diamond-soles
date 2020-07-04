using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Blank, Rock, Spawn, Stair, Hole, Iron, Diamond, Jelly, Third}

public class Tile
{

    private TileType _tileType;
    public TileType TileType
    {
        get { return _tileType; }
    }

    private int _playerID;

    public Tile(TileType t, int id)
    {
        _tileType = t;
        _playerID = id;


    }

}
