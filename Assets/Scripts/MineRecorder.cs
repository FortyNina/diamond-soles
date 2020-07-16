using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRecorder : MonoBehaviour
{

    private static Tile[][] ironMineFloors = new Tile[1000][];
    private static Tile[][] jellyMineFloors = new Tile[1000][];
    private static Tile[][] thirdMineFloors = new Tile[1000][];

    //update this when changing grid size
    private static bool[] ironGridCompleteFlags = new bool[4];
    private static bool[] jellyGridCompleteFlags = new bool[4];
    private static bool[] thirdGridCompleteFlags = new bool[4];

    //update this on number of mines
    private static bool ironDirty;
    public static bool IronDirty{
        get{return ironDirty;}
    }

    private static bool jellyDirty;
    public static bool JellyDirty
    {
        get { return jellyDirty; }
    }

    private static bool thirdDirty;
    public static bool ThirdDirty
    {
        get { return thirdDirty; }
    }

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

    private static Tile GetRandomTile(int index, int blank, int basic, int iron, int jelly, int third, int diamond)
    {
        int rand = Random.Range(0, 100);
        int marker = 0;

        for (int i = marker; i < blank + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Blank, 0, index);
        }
        marker += blank;
        for (int i = marker; i < basic + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Rock, 0, index);
        }
        marker += basic;
        for (int i = marker; i < iron + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Iron, 0, index);
        }
        marker += iron;
        for (int i = marker; i < jelly + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Food, 0, index);
        }
        marker += jelly;
        for (int i = marker; i < third + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Third, 0, index);
        }
        marker += third;
        for (int i = marker; i < diamond + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Diamond, 0, index);
        }
        marker += diamond;




        return new Tile(TileType.Blank, 0, index);

    }

    public static Tile[] CreateMineFloor(Mine mineType, int floor, int gridWidth, int gridHeight)
    {

        int percentBlank = 0;
        int percentBasic = 0;
        int percentIron = 0;
        int percentJelly = 0;
        int percentThird = 0;
        int percentDiamond = 0;

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
            else if (floor < 40)
            {
                percentBlank = 75;
                percentBasic = 11;
                percentIron = 11;
                percentDiamond = 3;
            }
            else if (floor < 50)
            {
                percentBlank = 75;
                percentBasic = 5;
                percentIron = 11;
                percentDiamond = 9;
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
            else if (floor < 40)
            {
                percentBlank = 75;
                percentBasic = 9;
                percentJelly = 11;
                percentDiamond = 5;
            }
            else if (floor < 50)
            {
                percentBlank = 75;
                percentBasic = 5;
                percentJelly = 11;
                percentDiamond = 9;
            }
        }

        //-----------------------------------------------------------------------------------------------------

        Tile[] newTiles = new Tile[gridWidth * gridHeight];

        for (int i = 0; i < newTiles.Length; i++)
        {

            newTiles[i] = GetRandomTile(i, percentBlank, percentBasic, percentIron, percentJelly, percentThird, percentDiamond);
        }

        int rand = Random.Range(0, newTiles.Length);
        int stairLoc = rand;
        int spawnLoc = rand;

        while (stairLoc == spawnLoc)
        {
            spawnLoc = Random.Range(0, newTiles.Length);
        }

        newTiles[stairLoc] = new Tile(TileType.Stair, 0, stairLoc);
        newTiles[spawnLoc] = new Tile(TileType.Spawn, 0, spawnLoc);

        if (mineType == Mine.IronMine)
            ironMineFloors[floor] = newTiles;
        if (mineType == Mine.JellyMine)
            jellyMineFloors[floor] = newTiles;
        if (mineType == Mine.ThirdMine)
            thirdMineFloors[floor] = newTiles;

        return newTiles;

    }


    public static void UpdateMineTileHealth(int floor, Mine mineType, int newHealth, int index)
    {
        if (mineType == Mine.IronMine)
        {
            if (ironMineFloors[floor] != null)
                ironMineFloors[floor][index].health += newHealth;
        }
        if (mineType == Mine.JellyMine)
        {
            if (jellyMineFloors[floor] != null)
                jellyMineFloors[floor][index].health += newHealth;
        }
        if (mineType == Mine.ThirdMine)
        {
            if (thirdMineFloors[floor] != null)
                thirdMineFloors[floor][index].health += newHealth;
        }
    }

    public static int GetMineTileHealth(int floor, Mine mineType, int index)
    {
        if (mineType == Mine.IronMine)
        {
            if (ironMineFloors[floor] != null)
                return ironMineFloors[floor][index].health;
        }
        if (mineType == Mine.JellyMine)
        {
            if (jellyMineFloors[floor] != null)
                return jellyMineFloors[floor][index].health;
        }
        if (mineType == Mine.ThirdMine)
        {
            if (thirdMineFloors[floor] != null)
                return thirdMineFloors[floor][index].health;
        }
        return 0;
    }


    public static void UpdateTileType(int floor, int index, Mine mineType, TileType newType)
    {

        if (mineType == Mine.IronMine)
        {
            if (ironMineFloors[floor] != null)
                ironMineFloors[floor][index].tileType = newType;
            ironDirty = true;
            for (int i = 0; i < ironGridCompleteFlags.Length; i++)
            {
                ironGridCompleteFlags[i] = false;
            }
        }
        if (mineType == Mine.JellyMine)
        {
            if (jellyMineFloors[floor] != null)
                jellyMineFloors[floor][index].tileType = newType;
            jellyDirty = true;
            for (int i = 0; i < jellyGridCompleteFlags.Length; i++)
            {
                jellyGridCompleteFlags[i] = false;
            }
        }
        if (mineType == Mine.ThirdMine)
        {
            if (thirdMineFloors[floor] != null)
                thirdMineFloors[floor][index].tileType = newType;
            thirdDirty = true;
            for (int i = 0; i < jellyGridCompleteFlags.Length; i++)
            {
                jellyGridCompleteFlags[i] = false;
            }
        }

    }

    public static bool CheckFlag(Mine mineType, int index)
    {
        if (mineType == Mine.IronMine)
            return ironGridCompleteFlags[index];
        if (mineType == Mine.JellyMine)
            return jellyGridCompleteFlags[index];
        if (mineType == Mine.ThirdMine)
            return thirdGridCompleteFlags[index];

        return false;

    }

    public static void SetBackFlag(Mine mineType, int index)
    {
        if (mineType == Mine.IronMine)
        {
            ironGridCompleteFlags[index] = true;
            bool done = true;
            for (int i = 0; i < ironGridCompleteFlags.Length; i++)
            {
                if (ironGridCompleteFlags[i] == false)
                    done = false;
            }
            if (done)
                ironDirty = false;
        }

        if (mineType == Mine.JellyMine)
        {
            jellyGridCompleteFlags[index] = true;
            bool done = true;
            for (int i = 0; i < jellyGridCompleteFlags.Length; i++)
            {
                if (jellyGridCompleteFlags[i] == false)
                    done = false;
            }
            if (done)
                jellyDirty = false;
        }

        if (mineType == Mine.ThirdMine)
        {
            thirdGridCompleteFlags[index] = true;
            bool done = true;
            for (int i = 0; i < thirdGridCompleteFlags.Length; i++)
            {
                if (thirdGridCompleteFlags[i] == false)
                    done = false;
            }
            if (done)
                thirdDirty = false;
        }
    }



}
