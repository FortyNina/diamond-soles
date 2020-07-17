using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorData
{

    public Mine mineType;
    public int floor;
    public int ownerID;
    public int price;

    public ElevatorData(Mine type, int flr, int id)
    {
        floor = flr;
        mineType = type;
        price = GetPrice();
        ownerID = id;
    }

    private int GetPrice()
    {
        if (floor < 20)
            return 80;
        if (floor < 30)
            return 100;
        if (floor < 40)
            return 140;
        if (floor < 50)
            return 175;
        if (floor < 60)
            return 200;
        return 250;

    }

   
}
