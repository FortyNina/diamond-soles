using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIPersonality { Basic, Traverser }


public class AIPersonalityPreferences : MonoBehaviour
{

    public static List<TileType> GetTilePreferences(AIPersonality personality)
    {
        List<TileType> types = new List<TileType>();

        if(personality == AIPersonality.Basic)
        {
            types.Add(TileType.Iron);
            types.Add(TileType.Food);
            types.Add(TileType.Coal);
            types.Add(TileType.Diamond);
        }

        if(personality == AIPersonality.Traverser)
        {
            types.Add(TileType.Hole);
            types.Add(TileType.Rock);
            types.Add(TileType.Diamond);
        }

        return types;
    }


}
