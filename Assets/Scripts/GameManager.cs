using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Game Mode Settings")]
    public bool performAuctionPhase = true;
    public bool giveAIRandomOre = true;
    public bool countEnergy = true;
    public bool countDurability = true;

    [Space (5)]
    [Header("Game Levers")]
    public int amountOfResourceToLoseAfterDay = 10;
    public float energyDrainFactor = .5f;


    public static bool subtractDurability;
    public GameObject[] players;

    
   
    // Start is called before the first frame update
    void Awake()
    {
        MiningStateSetup();

    }

    private void Update()
    {
        subtractDurability = countDurability;
        bool stillActive = false;
        for (int i = 0; i < GameData.Instance.energyLevels.Count; i++)
        {
            GameData.Instance.energyLevels[i] -= Time.deltaTime * energyDrainFactor;
            if (GameData.Instance.energyLevels[i] > 0)
                stillActive = true;
            else
            {
                if(countEnergy)players[i].GetComponent<PlayerController>().RunOutOfEnergy();
            }
        }
        //TODO: remove P?
        if ((!stillActive && performAuctionPhase) || Input.GetKeyDown(KeyCode.P))
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
                    GameData.Instance.playerOreSupplies[i][TileType.Food] += Random.Range(0, 2);
                    GameData.Instance.playerOreSupplies[i][TileType.Coal] += Random.Range(0, 2);
                }
                else if(rand == 1)
                {
                    GameData.Instance.playerOreSupplies[i][TileType.Food] += Random.Range(5, 15);
                    GameData.Instance.playerOreSupplies[i][TileType.Iron] += Random.Range(0, 2);
                    GameData.Instance.playerOreSupplies[i][TileType.Coal] += Random.Range(0, 2);
                }
                else
                {
                    GameData.Instance.playerOreSupplies[i][TileType.Coal] += Random.Range(5, 15);
                    GameData.Instance.playerOreSupplies[i][TileType.Food] += Random.Range(0, 2);
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
            GameData.Instance.playerOreSupplies[i][TileType.Food] -= amountOfResourceToLoseAfterDay;
            GameData.Instance.playerOreSupplies[i][TileType.Coal] -= amountOfResourceToLoseAfterDay;
        }


        //based on how much food you have, calculate your energy level for this day
        for (int i = 0; i < GameData.Instance.energyLevels.Count; i++)
        {
            int jelly = GameData.Instance.playerOreSupplies[i][TileType.Food];
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
        for (int i = 0; i < GameData.Instance.coalLevels.Count; i++)
        {
            int coal = GameData.Instance.playerOreSupplies[i][TileType.Coal];
            int newCoal = (100 * coal) / 200;
            GameData.Instance.coalLevels[i] = newCoal;
        }
    }
}
