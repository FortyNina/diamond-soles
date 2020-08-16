using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract
{
    public int rewardMoney;
    public Dictionary<TileType, int> requirements = new Dictionary<TileType, int>();
    public int numRounds;

    public string contractTitle = "";
    public string contractDescription = "";

    public Contract() {
        CreateRandomContract();
    }

    private void CreateRandomContract()
    {
        rewardMoney = Random.Range(500, 1500);
        numRounds = 1;

        TileType tile;
        int rand = Random.Range(0, 3);
        if (rand == 1)
        {
            tile = TileType.Iron;
        }
        else if (rand == 2)
        {
            tile = TileType.Coal;
        }
        else
        {
            tile = TileType.Food;
        }

        requirements.Add(tile, Random.Range(10, 30));
        contractTitle = tile.ToString() + " Needed!";
        contractDescription = "Pls help i need some " + tile.ToString() + " but im too scared to go into a mine by myself.";


    }


}
