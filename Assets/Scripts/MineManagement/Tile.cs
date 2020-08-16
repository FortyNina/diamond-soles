using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Blank, Rock, Spawn, Stair, Hole, Iron, Diamond, Food, Coal}

public class Tile
{

    public TileType tileType;
   
    public int oreAmount;
    public int health;

    public int index;

    public Tile(TileType t, int id, int index)
    {
        tileType = t;

        if(t == TileType.Rock)
        {
            oreAmount = 0;
            health = 1;
        }
        if(t == TileType.Iron)
        {
            oreAmount = Random.Range(8,10);
            health = 2;
        }
        if(t == TileType.Food)
        {
            oreAmount = Random.Range(8, 10);
            health = 2;
        }
        if (t == TileType.Diamond)
        {
            oreAmount = 10;
            health = 5;
        }
        if(t == TileType.Coal)
        {
            oreAmount = Random.Range(8, 10);
            health = 3;
        }

        this.index = index;


    }



}
