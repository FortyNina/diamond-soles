using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRecorder : MonoBehaviour
{

    public static Tile[][] ironMineFloors = new Tile[1000][];
    public static Tile[][] jellyMineFloors = new Tile[1000][];
    public static Tile[][] thirdMineFloors = new Tile[1000][];


    public static bool CheckMineFloorExists(Mine mineType, int floor)
    {
        if(mineType == Mine.IronMine)
        {
            if (ironMineFloors[floor] != null)
                return true;
        }
        if (mineType == Mine.JellyMine)
        {
            if (jellyMineFloors[floor] != null)
                return true;
        }
        if (mineType == Mine.ThirdMine)
        {
            if (thirdMineFloors[floor] != null)
                return true;
        }

        return false;
    }

    public static Tile[] GetMineFloor(Mine mineType, int floor)
    {
        if (mineType == Mine.IronMine)
        {
            if (ironMineFloors[floor] != null)
                return ironMineFloors[floor];
        }
        if (mineType == Mine.JellyMine)
        {
            if (jellyMineFloors[floor] != null)
                return jellyMineFloors[floor];
        }
        if (mineType == Mine.ThirdMine)
        {
            if (thirdMineFloors[floor] != null)
                return thirdMineFloors[floor];
        }
        return new Tile[0];
    }

    private static Tile GetRandomTile(int blank, int basic, int iron, int jelly, int third)
    {
        int rand = Random.Range(0, 100);
        int marker = 0;

        for (int i = marker; i < blank + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Blank, 0);
        }
        marker += blank;
        for (int i = marker; i < basic + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Rock, 0);
        }
        marker += basic;
        for (int i = marker; i < iron + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Iron, 0);
        }
        marker += iron;
        for (int i = marker; i < jelly + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Jelly, 0);
        }
        marker += jelly;
        for (int i = marker; i < third + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Third, 0);
        }
        marker += third;



        return new Tile(TileType.Blank, 0);

    }

    public static Tile[] CreateMineFloor(Mine mineType, int floor, int gridWidth, int gridHeight)
    {

        int percentBlank = 0;
        int percentBasic = 0;
        int percentIron = 0;
        int percentJelly = 0;
        int percentThird = 0;

        if (mineType == Mine.IronMine)
        {
            if (floor < 10)
            {
                percentBlank = 90;
                percentBasic = 7;
                percentIron = 3;
            }
            else if (floor < 20)
            {
                percentBlank = 80;
                percentBasic = 13;
                percentIron = 7;
            }
            else if (floor < 30)
            {
                percentBlank = 80;
                percentBasic = 9;
                percentIron = 11;
            }
        }

        else if (mineType == Mine.JellyMine)
        {
            if (floor < 10)
            {
                percentBlank = 90;
                percentBasic = 7;
                percentJelly = 3;
            }
            else if (floor < 20)
            {
                percentBlank = 80;
                percentBasic = 13;
                percentJelly = 7;
            }
            else if (floor < 30)
            {
                percentBlank = 80;
                percentBasic = 9;
                percentJelly = 11;
            }
        }

        //-----------------------------------------------------------------------------------------------------

        Tile[] newTiles = new Tile[gridWidth * gridHeight];

        for (int i = 0; i < newTiles.Length; i++)
        {

            newTiles[i] = GetRandomTile(percentBlank, percentBasic, percentIron, percentJelly, percentThird);
        }

        int rand = Random.Range(0, newTiles.Length);
        int stairLoc = rand;
        int spawnLoc = rand;

        while (stairLoc == spawnLoc)
        {
            spawnLoc = Random.Range(0, newTiles.Length);
        }

        newTiles[stairLoc] = new Tile(TileType.Stair, 0);
        newTiles[spawnLoc] = new Tile(TileType.Spawn, 0);

        if (mineType == Mine.IronMine)
            ironMineFloors[floor] = newTiles;
        if (mineType == Mine.JellyMine)
            jellyMineFloors[floor] = newTiles;
        if (mineType == Mine.ThirdMine)
            thirdMineFloors[floor] = newTiles;

        return newTiles;

    }


}
