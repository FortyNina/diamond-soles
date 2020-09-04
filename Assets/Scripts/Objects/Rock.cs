using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : FloorTile
{

    public GameObject holePrefab;

    public void CreateSettings(int floor, Mine mine, int index, Tile tile)
    {
        floorNumber = floor;
        mineType = mine;
        myTile = tile;
        this.index = index;
    }
}
