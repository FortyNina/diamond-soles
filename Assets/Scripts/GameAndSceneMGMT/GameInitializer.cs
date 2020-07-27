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

        //ONLY DO THIS SHIT ONCE AT THE BEGINNING OF THE GAME!
        //TODO: MOve this block to a title sCreen that happens when play is clicked?
        if (!GameData.Instance.setUpComplete)
        {
            for (int i = 0; i < GameData.Instance.numPlayers; i++)
            {
                //DEFINE AI PERSONALITIES
                if (i == 2) GameData.Instance.AIs.Add(AIPersonality.Traverser);
                else if(i == 3) GameData.Instance.AIs.Add(AIPersonality.Basic);
                else GameData.Instance.AIs.Add(AIManager.GetRandomPersonality());

                Dictionary<Mine, int> floors = new Dictionary<Mine, int>();
                floors.Add(Mine.IronMine, 0);
                floors.Add(Mine.JellyMine, 0);
                floors.Add(Mine.CoalMine, 0);
                GameData.Instance.playerFloors.Add(floors);

               
                Dictionary<TileType, int> ores = new Dictionary<TileType, int>();
                ores.Add(TileType.Iron, 0);
                ores.Add(TileType.Diamond, 0);
                ores.Add(TileType.Food, 0);
                ores.Add(TileType.Coal, 0);
                GameData.Instance.playerOreSupplies.Add(ores);

                GameData.Instance.durabilityLevels.Add(60);
                GameData.Instance.energyLevels.Add(60);
                GameData.Instance.coalLevels.Add(60);

                GameData.Instance.playerMoney.Add(1000);

                GameData.Instance.playerLocalLocations.Add(Vector3.zero);

                GameData.Instance.playerMineLocations.Add(Mine.Entry);

                GameData.Instance.playerElevators.Add(new List<ElevatorData>());

                GameData.Instance.gridLocations.Add(Vector3.zero);




            }

            GameData.Instance.familyMoney = 1000;
            GameData.Instance.familyOreSupplies[TileType.Iron] = 40;
            GameData.Instance.familyOreSupplies[TileType.Coal] = 40;
            GameData.Instance.familyOreSupplies[TileType.Food] = 40;

            GameData.Instance.setUpComplete = true;
        }

    }
}
