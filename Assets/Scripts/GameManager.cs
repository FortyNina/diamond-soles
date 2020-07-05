using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Mining, Auction}

public class GameManager : MonoBehaviour
{
    public static GameState state;


    public int amountOfResourceToLoseAfterDay = 10;

    [SerializeField]
    private PlayerController[] _playersInScene;

    // Start is called before the first frame update
    void Awake()
    {
        state = GameState.Mining;

        for(int i = 0; i < _playersInScene.Length; i++)
        {
            GameData.Instance.players.Add(_playersInScene[i]);
            GameData.Instance.ironFloors.Add(0);
            GameData.Instance.jellyFloors.Add(0);
            GameData.Instance.thirdFloors.Add(0);

            Dictionary<TileType, int> ores = new Dictionary<TileType, int>();

            ores.Add(TileType.Iron, 50);
            ores.Add(TileType.Diamond, 0);
            ores.Add(TileType.Jelly, 50);
            ores.Add(TileType.Third, 50);

            GameData.Instance.playerOreSupplies.Add(ores);

            GameData.Instance.durabilityLevels.Add(50);
            GameData.Instance.energyLevels.Add(50);
            GameData.Instance.thirdLevels.Add(50);

            GameData.Instance.playerMoney.Add(1000);
        }

        MiningStateSetup();

    }

    private void Update()
    {
        if(state == GameState.Mining)
        {
            bool stillActive = false;
            for(int i = 0;i < GameData.Instance.energyLevels.Count; i++)
            {
                GameData.Instance.energyLevels[i] -= Time.deltaTime;
                if (GameData.Instance.energyLevels[i] > 0)
                    stillActive = true;
                else
                {
                    //set that player inactive
                }
            }
            if (!stillActive)
            {

                state = GameState.Auction;
            }
        }


        else if(state == GameState.Auction)
        {

            state = GameState.Mining;
            MiningStateSetup();
            for(int i = 0; i < GameData.Instance.playerOreSupplies.Count;i++)
            {
                GameData.Instance.playerOreSupplies[i][TileType.Iron] -= amountOfResourceToLoseAfterDay;
                GameData.Instance.playerOreSupplies[i][TileType.Jelly] -= amountOfResourceToLoseAfterDay;
                GameData.Instance.playerOreSupplies[i][TileType.Third] -= amountOfResourceToLoseAfterDay;


            }

        }

    }


    //Determine stats
    private void MiningStateSetup()
    {
        //based on how much food you have, calculate your energy level for this day
        for(int i = 0; i < GameData.Instance.energyLevels.Count; i++)
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
