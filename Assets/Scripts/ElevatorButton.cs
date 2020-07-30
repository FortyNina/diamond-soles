using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{

    public void CreateElevator(int playerID)
    {
        Create(playerID);
    }

    public void CreateElevator()
    {
        Create(GameData.Instance.playerInFocus);
    }


    public void Create(int playerID) {
        GameData.Instance.playerOreSupplies[playerID][TileType.Coal] -= 20;
        Mine m = GameData.Instance.playerMineLocations[playerID];
        int floor = GameData.Instance.playerFloors[playerID][m];
        ElevatorData ed = new ElevatorData(m, floor, playerID);
        GameData.Instance.playerElevators[playerID].Add(ed);
    }
}
