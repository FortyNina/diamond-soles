using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public bool performAuctionPhase = true;

    public bool giveAIRandomOre = true;


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
        if (!stillActive || Input.GetKeyDown(KeyCode.P))
        {
            AuctionStateSetup();
        }
    }

    private void AuctionStateSetup()
    {

        //GIVE AI SOME RANDOM NUMBERS!
        if (giveAIRandomOre)
        {
            for(int i = GameData.Instance.numberRealPlayers;i<GameData.Instance.AIs.Count;i++)
            {
                int rand = Random.Range(0, 3);
                if(rand == 0)
                {
                    GameData.Instance.playerOreSupplies[i][TileType.Iron] += Random.Range(5, 15);
                    GameData.Instance.playerOreSupplies[i][TileType.Jelly] += Random.Range(0, 2);
                    GameData.Instance.playerOreSupplies[i][TileType.Third] += Random.Range(0, 2);
                }
                else if(rand == 1)
                {
                    GameData.Instance.playerOreSupplies[i][TileType.Jelly] += Random.Range(5, 15);
                    GameData.Instance.playerOreSupplies[i][TileType.Iron] += Random.Range(0, 2);
                    GameData.Instance.playerOreSupplies[i][TileType.Third] += Random.Range(0, 2);
                }
                else
                {
                    GameData.Instance.playerOreSupplies[i][TileType.Third] += Random.Range(5, 15);
                    GameData.Instance.playerOreSupplies[i][TileType.Jelly] += Random.Range(0, 2);
                    GameData.Instance.playerOreSupplies[i][TileType.Iron] += Random.Range(0, 2);
                }

            }
        }

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
