using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    

    public GameObject[] players;

    private void OnEnable()
    {
        Reset();
    }

    public void GoToAuction()
    {

        for(int i = 0; i < players.Length; i++)
        {
            GameData.Instance.co.oreSupplies[TileType.Iron] += GameData.Instance.playerOreSupplies[i][TileType.Iron];
            GameData.Instance.co.oreSupplies[TileType.Food] += GameData.Instance.playerOreSupplies[i][TileType.Food];
            GameData.Instance.co.oreSupplies[TileType.Coal] += GameData.Instance.playerOreSupplies[i][TileType.Coal];

        }

        SceneManager.GoToNextSceneInLoop();
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

    public void PullMiner()
    {
        AIMinerController aic = players[GameData.Instance.playerInFocus].GetComponent<AIMinerController>();
        aic.PullThisMiner();
    }

    public void PullAllMiners()
    {
        for(int i = 0; i < players.Length; i++)
        {
            AIMinerController aic = players[i].GetComponent<AIMinerController>();
            aic.PullThisMiner();

        }
        StartCoroutine(GoToAuctionAfterDelay(2f));
    }

    private IEnumerator GoToAuctionAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GoToAuction();
    }


}
