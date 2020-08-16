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



}
