using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Blank, Rock, Spawn, Stair, Hole, Iron, Diamond, Food, Third}

public class Tile
{

    public TileType tileType;
    
    private int _playerID;

    public int oreAmount;
    public int health;

    public int index;

    public Tile(TileType t, int id, int index)
    {
        tileType = t;
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
        if(t == TileType.Food)
        {
            oreAmount = 1;
            health = 2;
        }
        if (t == TileType.Diamond)
        {
            oreAmount = 5;
            health = 5;
        }

        this.index = index;


    }



}
