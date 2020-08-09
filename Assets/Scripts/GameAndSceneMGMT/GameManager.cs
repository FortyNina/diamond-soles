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

    }

    private void AuctionStateSetup()
    {

        for(int i = 0; i < players.Length; i++)
        {
            GameData.Instance.familyOreSupplies[TileType.Iron] += GameData.Instance.playerOreSupplies[i][TileType.Iron];
            GameData.Instance.familyOreSupplies[TileType.Food] += GameData.Instance.playerOreSupplies[i][TileType.Food];
            GameData.Instance.familyOreSupplies[TileType.Coal] += GameData.Instance.playerOreSupplies[i][TileType.Coal];

        }

        SceneManager.LoadScene("AuctionPhase");
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
