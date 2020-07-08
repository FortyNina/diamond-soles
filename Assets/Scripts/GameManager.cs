using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public bool performAuctionPhase = true;


    public int amountOfResourceToLoseAfterDay = 10;

    public float energyDrainFactor = .5f;

   
    // Start is called before the first frame update
    void Awake()
    {
        


        MiningStateSetup();

    }

    private void Update()
    {

        bool stillActive = false;
        for (int i = 0; i < GameData.Instance.energyLevels.Count; i++)
        {
            GameData.Instance.energyLevels[i] -= Time.deltaTime * energyDrainFactor;
            if (GameData.Instance.energyLevels[i] > 0)
                stillActive = true;
            else
            {
                //set that player inactive
            }
        }
        if (!stillActive)
        {
            AuctionStateSetup();
        }
    }

    private void AuctionStateSetup()
    {

        SceneManager.LoadScene("AuctionScene");
    }


    //Determine stats
    private void MiningStateSetup()
    {

        //End of day removals
        for (int i = 0; i < GameData.Instance.playerOreSupplies.Count; i++)
        {
            GameData.Instance.playerOreSupplies[i][TileType.Iron] -= amountOfResourceToLoseAfterDay;
            GameData.Instance.playerOreSupplies[i][TileType.Jelly] -= amountOfResourceToLoseAfterDay;
            GameData.Instance.playerOreSupplies[i][TileType.Third] -= amountOfResourceToLoseAfterDay;
        }


        //based on how much food you have, calculate your energy level for this day
        for (int i = 0; i < GameData.Instance.energyLevels.Count; i++)
        {
            int jelly = GameData.Instance.playerOreSupplies[i][TileType.Jelly];
            int newEnergy = (100 * jelly) / 200;
            GameData.Instance.energyLevels[i] = newEnergy;
        }

        //based on how much iron you have, calculate your axe durability level for this day
        for (int i = 0; i < GameData.Instance.durabilityLevels.Count; i++)
        {
            int iron = GameData.Instance.playerOreSupplies[i][TileType.Iron];
            int newDurability = (100 * iron) / 200;
            GameData.Instance.durabilityLevels[i] = newDurability;
        }

        //based on how much iron you have, calculate your axe durability level for this day
        for (int i = 0; i < GameData.Instance.thirdLevels.Count; i++)
        {
            int third = GameData.Instance.playerOreSupplies[i][TileType.Third];
            int newThird = (100 * third) / 200;
            GameData.Instance.thirdLevels[i] = newThird;
        }

        

    }

}
