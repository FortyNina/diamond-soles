using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsSetter : MonoBehaviour
{

    public bool countEnergy = true;
    public bool countDurability = false;
    public bool countCoal = false;

    [Space(5)]

    public bool performAuctionPhase = false;
    public bool giveAIRandomOre = false;

    [Space(10)]
    public float energyDrainFactor = .5f;
    public float maxEnergy = 60;
    public float maxDurability = 60;
    public float maxCoal = 60;


    // Update is called once per frame
    void Update()
    {
        GameSettings.Instance.countEnergy = countEnergy;
        GameSettings.Instance.countDurability = countDurability;
        GameSettings.Instance.countCoal = countCoal;
        GameSettings.Instance.performAuctionPhase = performAuctionPhase;
        GameSettings.Instance.giveAIRandomOre = giveAIRandomOre;
        GameSettings.Instance.energyDrainFactor = energyDrainFactor;
        GameSettings.Instance.maxEnergy = maxEnergy;
        GameSettings.Instance.maxDurability = maxDurability;
        GameSettings.Instance.maxCoal = maxCoal;

    }
}
