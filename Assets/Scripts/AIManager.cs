using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIPersonality { Basic }

public class AIManager : MonoBehaviour
{

    public static AIPersonality GetRandomPersonality()
    {
        int rand = Random.Range(0, 1);
        if (rand == 0)
            return AIPersonality.Basic;
        else
            return AIPersonality.Basic;
    }
    

    public static bool DetermineBuyer(int playerIndex, TileType ore)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        if(ore == TileType.Iron)
        {
            if(GameData.Instance.playerOreSupplies[playerIndex][TileType.Iron] > 40)
            {
                return true;
            }
        }

        return false;
    }
    
    
}
