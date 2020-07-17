using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorObj : MonoBehaviour
{
    public Color[] playerColors;

    public int playerID;
    public int floor;
    public int price;

    public void SetData(ElevatorData ed)
    {
        playerID = ed.ownerID;
        floor = ed.floor;
        price = ed.price;
        GetComponent<SpriteRenderer>().color = playerColors[playerID];
             
    }
}
