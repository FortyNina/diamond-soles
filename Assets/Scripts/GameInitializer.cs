using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    private void Awake()
    {
        //ONLY DO THIS SHIT ONCE AT THE BEGINNING OF THE GAME!
        //TODO: MOve this block to a title sCreen that happens when play is clicked?
        if (!GameData.Instance.setUpComplete)
        {
            for (int i = 0; i < GameData.Instance.numPlayers; i++)
            {
                //DEFINE AI PERSONALITIES
                GameData.Instance.AIs.Add(AIManager.GetRandomPersonality());


                GameData.Instance.ironFloors.Add(0);
                GameData.Instance.jellyFloors.Add(0);
                GameData.Instance.thirdFloors.Add(0);

                Dictionary<TileType, int> ores = new Dictionary<TileType, int>();

                ores.Add(TileType.Iron, 50);
                ores.Add(TileType.Diamond, 0);
                ores.Add(TileType.Food, 50);
                ores.Add(TileType.Third, 50);

                GameData.Instance.playerOreSupplies.Add(ores);

                GameData.Instance.durabilityLevels.Add(60);
                GameData.Instance.energyLevels.Add(60);
                GameData.Instance.thirdLevels.Add(60);

                GameData.Instance.playerMoney.Add(1000);

                GameData.Instance.playerLocalLocations.Add(Vector3.zero);

                GameData.Instance.playerMineLocations.Add(Mine.Entry);


            }
            GameData.Instance.setUpComplete = true;
        }

    }
}
