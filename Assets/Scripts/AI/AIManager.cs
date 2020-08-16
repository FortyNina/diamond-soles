using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] public enum AIPersonality { Basic, Explorer }

public class AIManager
{
    /// <summary>
    /// Return a randomly generated personality
    /// </summary>
    public static AIPersonality GetRandomPersonality()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            return AIPersonality.Explorer;
        else
            return AIPersonality.Basic;
    }

    public static List<TileType> GetTilePreferences(AIPersonality personality)
    {
        List<TileType> types = new List<TileType>();

        if (personality == AIPersonality.Basic)
        {
            types.Add(TileType.Iron);
            types.Add(TileType.Food);
            types.Add(TileType.Coal);
            types.Add(TileType.Diamond);
        }

        if (personality == AIPersonality.Explorer)
        {
            types.Add(TileType.Hole);
            types.Add(TileType.Rock);
            types.Add(TileType.Diamond);
            types.Add(TileType.Stair);

        }

        return types;
    }


    public static TileType GetTileTypeToSeek(int playerIndex, bool random)
    {
        if(GameData.Instance.durabilityLevels[playerIndex] <= 0) //if can't use axe anymore, just try traversing as much as possible
        {
            return TileType.Stair;
        }

        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        Mine mine = GameData.Instance.playerMineLocations[playerIndex];
        int floor = 0;
        Tile[] currentMineLayout = new Tile[0];
        if (!GameData.Instance.playerFloors[playerIndex].ContainsKey(mine)) return TileType.Stair;
        floor = GameData.Instance.playerFloors[playerIndex][mine];

        currentMineLayout = MineRecorder.GetMineFloor(mine, floor);
        Dictionary<TileType, int> tileKinds = new Dictionary<TileType, int>();
        List<TileType> preferences = GetTilePreferences(pers);
        for (int i = 0; i < currentMineLayout.Length; i++)
        {
            TileType t = currentMineLayout[i].tileType;

            if (preferences.Contains(t) || random)
            {
                if (tileKinds.ContainsKey(t))
                    tileKinds[t]++;
                else
                    tileKinds.Add(t, 1);
            }
        }

        if (pers == AIPersonality.Basic)
        {
            int rand = Random.Range(0, tileKinds.Count - 1);
            int index = 0;
            foreach (KeyValuePair<TileType, int> entry in tileKinds)
            {
                if (index == rand)
                    return entry.Key;
                index++;
            }
        }
        if (pers == AIPersonality.Explorer)
        {
            if (tileKinds.ContainsKey(TileType.Hole)) return TileType.Hole;
            int rand = Random.Range(0, 10);
            if (rand < 5) return TileType.Stair;
            return TileType.Rock;
        }


        return TileType.Stair;
    }

    public static Transform GetTargetedTileTransformFromMap(Collider2D[] interactableObjects, TileType toSeek, int playerIndex, Transform playerPos)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        List<Transform> tileTransforms = new List<Transform>();
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            if (toSeek == TileType.Stair && interactableObjects[i].gameObject.tag == "Staircase")
            {
                tileTransforms.Add(interactableObjects[i].transform);
            }
            if (toSeek == TileType.Hole && interactableObjects[i].gameObject.tag == "Hole")
            {
                tileTransforms.Add(interactableObjects[i].transform);
            }
            if (interactableObjects[i].GetComponent<Rock>() != null)
            {
                if (interactableObjects[i].GetComponent<Rock>().ore == toSeek)
                {
                    tileTransforms.Add(interactableObjects[i].transform);
                }
            }
        }

        //find random tile rn
        if (pers == AIPersonality.Basic)
        {

            int rand = Random.Range(0, tileTransforms.Count);
            if (rand < tileTransforms.Count)
                return tileTransforms[rand];
        }
        //get closest
        if (pers == AIPersonality.Explorer)
        {
            float minDist = 100;
            Transform minTransform = null;
            for (int i = 0; i < tileTransforms.Count; i++)
            {
                if (Vector3.Distance(playerPos.position, tileTransforms[i].position) < minDist)
                {
                    minDist = Vector3.Distance(playerPos.position, tileTransforms[i].position);
                    minTransform = tileTransforms[i];
                }
            }
            return minTransform;
        }
        return null;
    }

    public static Mine GetMineChoice(int playerIndex)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        if (pers == AIPersonality.Basic)
        {
            int rand = Random.Range(0, 3);
            if (rand == 0)
                return Mine.IronMine;
            else if (rand == 1)
                return Mine.JellyMine;
            return Mine.CoalMine;
        }
        if (pers == AIPersonality.Explorer)
            return Mine.JellyMine;

        return Mine.IronMine;

    }

    public static Transform ChooseElevator(int playerIndex, List<Collider2D> objs)
    {
        if (objs.Count == 0)
            return null;

        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        if (pers == AIPersonality.Basic || pers == AIPersonality.Explorer) //go to deepest mine :) 
        {
            int maxFloor = 0;
            Transform t = null;
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i].GetComponent<ElevatorObj>().floor > maxFloor)
                {
                    maxFloor = objs[i].GetComponent<ElevatorObj>().floor;
                    t = objs[i].transform;
                }
            }
            return t;

        }

        return null;
    }

    public static bool BuildElevator(int playerIndex)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        int currentFloor = GameData.Instance.playerFloors[playerIndex][GameData.Instance.playerMineLocations[playerIndex]];

        int currentElevator = 0;
        for (int i = 0; i < GameData.Instance.playerElevators[playerIndex].Count; i++)
        {
            if (GameData.Instance.playerElevators[playerIndex][i].mineType == GameData.Instance.playerMineLocations[playerIndex])
                currentElevator = GameData.Instance.playerElevators[playerIndex][i].floor;
        }

        if (currentFloor <= currentElevator) return false;
        if (GameData.Instance.playerOreSupplies[playerIndex][TileType.Coal] < 20) return false;

        if (pers == AIPersonality.Basic || pers == AIPersonality.Explorer)
        {
            if (currentFloor - currentElevator >= 10 && Random.Range(0, 3) > 1) return true;
        }

        return false;
    }

    
}
