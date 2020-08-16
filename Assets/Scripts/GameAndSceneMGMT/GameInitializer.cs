using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    private void Awake()
    {

        while(GameData.Instance.AIs.Count < GameData.Instance.numPlayers)
        {
            //DEFINE AI PERSONALITIES
            GameData.Instance.AIs.Add(AIManager.GetRandomPersonality());
        }

        while(GameData.Instance.playerFloors.Count < GameData.Instance.numPlayers)
        {
            Dictionary<Mine, int> floors = new Dictionary<Mine, int>();
            floors.Add(Mine.IronMine, 0);
            floors.Add(Mine.JellyMine, 0);
            floors.Add(Mine.CoalMine, 0);
            //floors.Add(Mine.Entry, 0);
            GameData.Instance.playerFloors.Add(floors);
        }

        while(GameData.Instance.playerOreSupplies.Count < GameData.Instance.numPlayers)
        {
            Dictionary<TileType, int> ores = new Dictionary<TileType, int>();
            ores.Add(TileType.Iron, 0);
            ores.Add(TileType.Diamond, 0);
            ores.Add(TileType.Food, 0);
            ores.Add(TileType.Coal, 0);
            GameData.Instance.playerOreSupplies.Add(ores);
        }

        while(GameData.Instance.durabilityLevels.Count < GameData.Instance.numPlayers)
        {
            GameData.Instance.durabilityLevels.Add(60);
        }

        while (GameData.Instance.energyLevels.Count < GameData.Instance.numPlayers)
        {
            GameData.Instance.energyLevels.Add(60);
        }

        while (GameData.Instance.coalLevels.Count < GameData.Instance.numPlayers)
        {
            GameData.Instance.coalLevels.Add(60);
        }

        while (GameData.Instance.playerMoney.Count < GameData.Instance.numPlayers)
        {
            GameData.Instance.playerMoney.Add(1000);
        }

        while (GameData.Instance.playerLocalLocations.Count < GameData.Instance.numPlayers)
        {
            GameData.Instance.playerLocalLocations.Add(Vector3.zero);
        }

        while (GameData.Instance.playerMineLocations.Count < GameData.Instance.numPlayers)
        {
            GameData.Instance.playerMineLocations.Add(Mine.Entry);
        }

        while (GameData.Instance.playerElevators.Count < GameData.Instance.numPlayers)
        {
            GameData.Instance.playerElevators.Add(new List<ElevatorData>());
        }

        while (GameData.Instance.gridLocations.Count < GameData.Instance.numPlayers)
        {
            GameData.Instance.gridLocations.Add(Vector3.zero);
        }

        
        if (!GameData.Instance.setUpComplete)
        {
            GameData.Instance.setUpComplete = true;

            //Note: index 0 is the player
            for(int i = 0; i < GameData.Instance.numAuctionAi + 1; i++)
            {
                Company c = new Company();

                c.personality = AIAuctionManager.GetRandomPersonality();

                Dictionary<TileType, int> ores = new Dictionary<TileType, int>();
                ores.Add(TileType.Iron, 40);
                ores.Add(TileType.Diamond, 0);
                ores.Add(TileType.Food, 40);
                ores.Add(TileType.Coal, 40);
                c.oreSupplies = ores;

                c.money = 1000;
                c.rating = 5;

                c.contract = new Contract();

                GameData.Instance.companies.Add(c);


            }

        }




    }
}
