using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handle collection of resources
/// And Durability, Coal, and Jelly levels!
/// </summary>

public class MinerResourceManager : MonoBehaviour
{
    [SerializeField]
    private AIMinerController minerController;
    private int _playerID;

    public UnityEvent OnRunOutOfEnergy;

    private void Start()
    {
        _playerID = minerController.playerID;
    }

    // Update is called once per frame
    void Update()
    {
        ModifyEnergyLevel(-Time.deltaTime * GameSettings.Instance.energyDrainFactor);
    }

    /// <summary>
    /// Call to add a resource to the playerOreSupplies dict.
    /// </summary>
    public void CollectResource(TileType resource, int amount)
    {
        GameData.Instance.playerOreSupplies[_playerID][resource] += amount;
    }

    /// <summary>
    /// Update this players energy level. Call with a negative amount to subtract
    /// </summary>
    public void ModifyEnergyLevel(float amount)
    {
         GameData.Instance.energyLevels[_playerID] += amount;
    }

    /// <summary>
    /// Update this players durability level. Call with a negative amount to subtract
    /// </summary>
    public void ModifyDurabilityLevel(int amount)
    {
        GameData.Instance.durabilityLevels[_playerID] += amount;
    }

    /// <summary>
    /// Update this players durability level. Call with a negative amount to subtract
    /// </summary>
    public void ModifyDurabilityLevel()
    {
        GameData.Instance.durabilityLevels[_playerID] += -1;
    }


    /// <summary>
    /// Update this players coal level. Call with a negative amount to subtract
    /// </summary>
    public void ModifyCoalLevel(int amount)
    {
        GameData.Instance.coalLevels[_playerID] += amount;
    }

    /// <summary>
    /// Calculate energy, durability, and coal levels from the amount of Iron, Jelly, and Coal
    /// a miner has.
    /// </summary>
    public void SetMinerLevelsFromOre()
    {
        //based on how much food you have, calculate your energy level for this day
        int jelly = GameData.Instance.playerOreSupplies[_playerID][TileType.Food];
        int newEnergy = (100 * jelly) / 200;
        GameData.Instance.energyLevels[_playerID] = newEnergy;

        //based on how much iron you have, calculate your axe durability level for this day
        int iron = GameData.Instance.playerOreSupplies[_playerID][TileType.Iron];
        int newDurability = (100 * iron) / 200;
        GameData.Instance.durabilityLevels[_playerID] = newDurability;

        //based on how much iron you have, calculate your axe durability level for this day
        int coal = GameData.Instance.playerOreSupplies[_playerID][TileType.Coal];
        int newCoal = (100 * coal) / 200;
        GameData.Instance.coalLevels[_playerID] = newCoal;
    }



}
