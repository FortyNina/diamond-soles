using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : Singleton<GameSettings>
{

    //Bools to turn modeds on/off
    public bool countEnergy = false;
    public bool countDurability = true;
    public bool countCoal = false;
    public bool performAuctionPhase = false;
    public bool giveAIRandomOre = false;


    public float energyDrainFactor = .5f;

}
