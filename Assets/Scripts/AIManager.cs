using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIPersonality { Basic }

public class AIManager : MonoBehaviour
{

    public static AIPersonality GetRandomPersonality()
    {
        int rand = Random.Range(0, 1);
        if (rand == 0)
            return AIPersonality.Basic;
        else
            return AIPersonality.Basic;
    }
    

    public static bool DetermineBuyer(int playerIndex, TileType ore)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        if(ore == TileType.Iron)
        {
            if (pers == AIPersonality.Basic)
            {
                if (GameData.Instance.playerOreSupplies[playerIndex][TileType.Iron] > 50)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static int GetSellPrice(int playerIndex, TileType ore)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        int oreAmount = GameData.Instance.playerOreSupplies[playerIndex][ore];

        if(pers == AIPersonality.Basic)
        {
            if (oreAmount > 90)
                return 20;
            if (oreAmount > 70)
                return 25;
            if (oreAmount > 50)
                return 28;
            if (oreAmount > 30)
                return 35;
            return 50;
        }

        return 50;
    }

    public static int GetBuyPrice(int playerIndex, TileType ore)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        int oreAmount = GameData.Instance.playerOreSupplies[playerIndex][ore];

        if (pers == AIPersonality.Basic)
        {
            if (oreAmount < 20)
                return 40;
            if (oreAmount < 40)
                return 30;
            if (oreAmount < 60)
                return 25;
            if (oreAmount < 80)
                return 20;
            return 15;
        }

        return 15;
    }

    public static TileType GetTileTypeToSeek(int playerIndex)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        Mine mine = GameData.Instance.playerMineLocations[playerIndex];
        int floor = 0;
        Tile[] currentMineLayout = new Tile[0] ;

        if (mine == Mine.IronMine)
        {
            floor = GameData.Instance.ironFloors[playerIndex];
        }

        if (mine == Mine.JellyMine)
        {
            floor = GameData.Instance.jellyFloors[playerIndex];
        }

        if (mine == Mine.ThirdMine)
        {
            floor = GameData.Instance.thirdFloors[playerIndex];
        }

        currentMineLayout = MineRecorder.GetMineFloor(mine, floor);





        if (pers == AIPersonality.Basic)
        {
            if (Random.Range(0, 2) == 1)
                return TileType.Iron;
            else
                return TileType.Rock;
        }

        return TileType.Stair;


    }


}
