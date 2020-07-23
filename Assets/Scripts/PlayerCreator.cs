using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerCreator : MonoBehaviour
{
    public float moveSpeed;
    public GridCreator gridCreator;
    public ElevatorButton elevatorCreator;
    public GameObject runOutOfEnergyDisplay;
    public GameObject axe;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;


    void Start()
    {

        PlayerController pc;

        if (gridCreator.playerID < GameData.Instance.numberRealPlayers)
        {
            pc = gameObject.AddComponent<PlayerController>();
            pc.IsAI = false;
            pc.SetControlKeys(up, down, left, right);
        }

        else
        {
            gameObject.AddComponent<Seeker>();
            pc = gameObject.AddComponent<AIPlayerController>();
            pc.IsAI = true;
        }

        pc.GridCreator = gridCreator;
        pc.PlayerID = gridCreator.playerID;
        axe.AddComponent<Axe>();
        axe.GetComponent<Axe>().PlayerID = gridCreator.playerID;
        pc.Axe = axe;

        pc.Speed = moveSpeed;
        pc.elevatorCreator = elevatorCreator;
        pc.runOutOfEnergyDisplay = runOutOfEnergyDisplay;

    }

   
}
