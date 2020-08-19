using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Company
{
    public string companyName = "";

    public Dictionary<TileType, int> oreSupplies;
    public int money;
    public float rating;

    public AIAuctionPersonality personality; // if AI

    public Contract contract;



    public Company() { }



    public void FulfillContract()
    {
        if (contract == null) return;

        money += contract.rewardMoney;
        float newRating = 0;
        foreach(KeyValuePair<TileType,int> entry in contract.requirements)
        {
            oreSupplies[entry.Key] -= entry.Value;
            newRating += ((float)entry.Value / 100f);
        }

        rating += newRating;

    }

    public void DeclineContract()
    {
        float newRating = 0;

        foreach (KeyValuePair<TileType, int> entry in contract.requirements)
        {
            newRating += ((float)entry.Value / 100f);
        }

        rating -= newRating;


    }


    //To be used on Ai companies to simulate their resources
    public void SimulateMiningPhase()
    {
        //Simulate resources that were allotted
        int allottedIron = (int)(oreSupplies[TileType.Iron] * Random.Range(.6f, 1));
        oreSupplies[TileType.Iron] -= allottedIron;

        int allottedFood = (int)(oreSupplies[TileType.Food] * Random.Range(.6f, 1));
        oreSupplies[TileType.Food] -= allottedFood;


        //int allottedCoal = (int)(oreSupplies[TileType.Coal] * Random.Range(.6f, 1));

        if (contract != null) 
        {
            if (contract.requirements.ContainsKey(TileType.Iron))
            {
                oreSupplies[TileType.Iron] += (int)((float)allottedIron * Random.Range(1f, 1.25f));
                oreSupplies[TileType.Food] += (int)((float)allottedFood * Random.Range(.8f, 1.1f));

            }
            else if(contract.requirements.ContainsKey(TileType.Food))
            {
                oreSupplies[TileType.Iron] += (int)((float)allottedIron * Random.Range(.8f, 1.1f));
                oreSupplies[TileType.Food] += (int)((float)allottedFood * Random.Range(1f, 1.25f));
            }
        }
        else
        {
            oreSupplies[TileType.Iron] += (int)((float)allottedIron * Random.Range(.9f, 1.1f));
            oreSupplies[TileType.Food] += (int)((float)allottedFood * Random.Range(.9f, 1.1f));
        }








    }



}
