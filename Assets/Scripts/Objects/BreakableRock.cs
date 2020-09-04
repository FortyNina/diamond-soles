using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableRock : Rock, IBreakable
{

    public int oreAmount;
    public int health;

    public void CreateSettings(int ore, int hea, int floor, Mine mine, int index, Tile tile)
    {
        oreAmount = ore;
        health = hea;
        floorNumber = floor;
        mineType = mine;
        myTile = tile;
        this.index = index;
    }


    public void Break()
    {
        if (Random.Range(0, 10) < 1)
        {
            GameObject go = Instantiate(holePrefab, transform.position, Quaternion.identity);
            go.transform.parent = transform.parent;
            go.transform.position = transform.position;
            MineRecorder.UpdateTileType(floorNumber, index, mineType, TileType.Hole);
        }
        else
        {
            MineRecorder.UpdateTileType(floorNumber, index, mineType, TileType.Blank);

        }
        AStarMapController.RequestScan();
    }

    


}
