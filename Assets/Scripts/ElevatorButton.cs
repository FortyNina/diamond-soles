using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public void CreateNewElevator(int playerID)
    {
        GameData.Instance.playerOreSupplies[playerID][TileType.Coal] -= 20;
        Mine m = GameData.Instance.playerMineLocations[playerID];
        int floor = 0;
        if (m == Mine.IronMine)
            floor = GameData.Instance.ironFloors[playerID];
        if (m == Mine.JellyMine)
            floor = GameData.Instance.jellyFloors[playerID];
        if (m == Mine.CoalMine)
            floor = GameData.Instance.coalFloors[playerID];

        ElevatorData ed = new ElevatorData(m, floor, playerID);
        GameData.Instance.playerElevators[playerID].Add(ed);



    }
}
