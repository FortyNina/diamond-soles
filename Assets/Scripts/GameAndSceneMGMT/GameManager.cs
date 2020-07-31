using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    

    public GameObject[] players;

    private void OnEnable()
    {
        Reset();
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.P))
        {
             AuctionStateSetup();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //drop floors?
           
            SceneManager.LoadScene("MiningPhase");
        }
    }

    private void AuctionStateSetup()
    {

        //GIVE AI SOME RANDOM NUMBERS!
        if (GameSettings.Instance.giveAIRandomOre)
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

    public void Reset()
    {
        for (int i = 0; i < GameData.Instance.playerFloors.Count; i++)
        {
            GameData.Instance.playerFloors[i][Mine.IronMine] = 0;
            GameData.Instance.playerFloors[i][Mine.JellyMine] = 0;
            GameData.Instance.playerFloors[i][Mine.CoalMine] = 0;

            GameData.Instance.playerMineLocations[i] = Mine.Entry;

        }
    }


}
