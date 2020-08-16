using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIAuctionPersonality { Robots, Humans, Cyborgs }


public class AIAuctionManager : MonoBehaviour
{
    /// <summary>
    /// Return a randomly generated personality
    /// </summary>
    public static AIAuctionPersonality GetRandomPersonality()
    { 
        return AIAuctionPersonality.Robots;
    }


    /// <summary>
    /// Will be used to select a contract from a list of contracts
    /// in future will probably account for personality/stats,
    /// but right now, just choose the one with the highest reward
    /// </summary>
    public static Contract ChooseContractFromList(List<Contract> contracts)
    {
        int index = 0;
        int maxPrice = -1;
        for(int i = 0;i < contracts.Count; i++)
        {
            if(contracts[i].rewardMoney > maxPrice)
            {
                index = i;
                maxPrice = contracts[i].rewardMoney;
            }
        }
        return contracts[index];
    }




    /// <summary>
    /// Return, based on the players ore and money, whether or not this character will
    /// be a buyer for a particular round in the auction
    /// </summary>
    public static bool DetermineBuyer(int playerIndex, TileType ore)
    {
        AIAuctionPersonality pers = GameData.Instance.companies[playerIndex].personality;
        if (ore == TileType.Iron)
        {
            if (pers == AIAuctionPersonality.Robots)
            {
                if (GameData.Instance.companies[playerIndex].oreSupplies[ore] > 50) return false;
            } 
        }

        return true;
    }

    /// <summary>
    /// Determine how much a player is willing to sell an ore for in a round of the auction
    /// </summary>
    public static int GetSellPrice(int playerIndex, TileType ore)
    {
        AIAuctionPersonality pers = GameData.Instance.companies[playerIndex].personality;
        int oreAmount = GameData.Instance.companies[playerIndex].oreSupplies[ore];

        if (pers == AIAuctionPersonality.Robots)
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
        AIAuctionPersonality pers = GameData.Instance.companies[playerIndex].personality;
        int oreAmount = GameData.Instance.companies[playerIndex].oreSupplies[ore];

        if (pers == AIAuctionPersonality.Robots)
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
