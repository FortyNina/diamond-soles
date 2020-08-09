using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIAuctionManager : MonoBehaviour
{
    /// <summary>
    /// Return a randomly generated personality
    /// </summary>
    public static AIAuctionPersonality GetRandomPersonality()
    { 
        return AIAuctionPersonality.Basic;
    }

    /// <summary>
    /// Return, based on the players ore and money, whether or not this character will
    /// be a buyer for a particular round in the auction
    /// </summary>
    public static bool DetermineBuyer(int playerIndex, TileType ore)
    {
        AIAuctionPersonality pers = GameData.Instance.auctionAIs[playerIndex];
        if (ore == TileType.Iron)
        {
            if (pers == AIAuctionPersonality.Basic)
            {
                if (GameData.Instance.auctionPlayerOreSupplies[playerIndex][ore] > 50) return false;
            } 
        }

        return true;
    }

    /// <summary>
    /// Determine how much a player is willing to sell an ore for in a round of the auction
    /// </summary>
    public static int GetSellPrice(int playerIndex, TileType ore)
    {
        AIAuctionPersonality pers = GameData.Instance.auctionAIs[playerIndex];
        int oreAmount = GameData.Instance.auctionPlayerOreSupplies[playerIndex][ore];

        if (pers == AIAuctionPersonality.Basic)
        {
            if (oreAmount > 90) return 20;
            if (oreAmount > 70) return 25;
            if (oreAmount > 50) return 28;
            if (oreAmount > 30) return 35;
            return 50;
        }
        

        return 50;
    }

    /// <summary>
    /// Determine how much money a player is wiliing to spend on an ore in the auction
    /// </summary>
    public static int GetBuyPrice(int playerIndex, TileType ore)
    {
        AIAuctionPersonality pers = GameData.Instance.auctionAIs[playerIndex];
        int oreAmount = GameData.Instance.auctionPlayerOreSupplies[playerIndex][ore];

        if (pers == AIAuctionPersonality.Basic)
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


}
