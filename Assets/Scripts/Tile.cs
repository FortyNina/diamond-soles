﻿using System.Collections;
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

    public int oreAmount;
    public int health;

    public int index;

    public Tile(TileType t, int id, int index)
    {
        _tileType = t;
        _playerID = id;

        if(t == TileType.Rock)
        {
            oreAmount = 0;
            health = 1;
        }
        if(t == TileType.Iron)
        {
            oreAmount = 1;
            health = 2;
        }
        if(t == TileType.Jelly)
        {
            oreAmount = 1;
            health = 2;
        }

        this.index = index;


    }

}
