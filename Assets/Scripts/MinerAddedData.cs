﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinerAddedData : MonoBehaviour
{

    [HideInInspector]
    public MinerSelectionScreen selection;

    public int playerID;


    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private TextMeshProUGUI _energyText;

    [SerializeField]
    private TextMeshProUGUI _durabilityText;

    private int _durability = 0;
    public int Durability{get{return _durability;}}

    private int _energy = 0;
    public int Energy { get { return _energy; } }

    private int _coal = 0;
    public int Coal { get { return _coal; } }

    private AIPersonality _pers;
    public AIPersonality Personality
    {
        get { return _pers; }
        set { _pers = value;}
    }




    // Update is called once per frame
    void Update()
    {
        _nameText.text = _pers.ToString() + " Miner " + playerID;
        _energyText.text = "Energy: " + _energy;
        _durabilityText.text = "Durability: " + _durability;

    }

    public void AddEnergy()
    {
        _energy += 5;
        GameData.Instance.co.oreSupplies[TileType.Food]-=5;
    }
    public void SubtractEnergy()
    {
        _energy -= 5;
        GameData.Instance.co.oreSupplies[TileType.Food]+=5;
    }

    public void AddDurability()
    {
        _durability += 5;
        GameData.Instance.co.oreSupplies[TileType.Iron]-=5;
    }
    public void SubtractDurability()
    {
        _durability -= 5;
        GameData.Instance.co.oreSupplies[TileType.Iron]+=5;
    }

    public void AddCoal()
    {
        _coal += 5;
        GameData.Instance.co.oreSupplies[TileType.Coal] -= 5;
    }
    public void SubtractCoal()
    {
        _coal -= 5;
        GameData.Instance.co.oreSupplies[TileType.Coal] += 5;
    }

    public void ReturnAllResources()
    {
        GameData.Instance.co.oreSupplies[TileType.Iron] += _durability;
        GameData.Instance.co.oreSupplies[TileType.Food] += _energy;
        GameData.Instance.co.oreSupplies[TileType.Coal] += _coal;
        RemoveThisMiner();

    }

    private void RemoveThisMiner()
    {
        selection.RemoveMiner(playerID);
    }



}
