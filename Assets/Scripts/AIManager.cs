﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIManager : MonoBehaviour
{
    /// <summary>
    /// Return a randomly generated personality
    /// </summary>
    public static AIPersonality GetRandomPersonality()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            return AIPersonality.Traverser;
        else
            return AIPersonality.Basic;
    }
    
    /// <summary>
    /// Return, based on the players ore and money, whether or not this character will
    /// be a buyer for a particular round in the auction
    /// </summary>
    public static bool DetermineBuyer(int playerIndex, TileType ore)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        if(ore == TileType.Iron)
        {
            if (pers == AIPersonality.Basic)
            {
                if (GameData.Instance.playerOreSupplies[playerIndex][ore] > 50) return false;  
            }
            if(pers == AIPersonality.Traverser)
            {
                if (ore == TileType.Food) return true;
                if (GameData.Instance.playerOreSupplies[playerIndex][ore] > 60) return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determine how much a player is willing to sell an ore for in a round of the auction
    /// </summary>
    public static int GetSellPrice(int playerIndex, TileType ore)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        int oreAmount = GameData.Instance.playerOreSupplies[playerIndex][ore];

        if(pers == AIPersonality.Basic)
        {
            if (oreAmount > 90) return 20;
            if (oreAmount > 70) return 25;
            if (oreAmount > 50) return 28;
            if (oreAmount > 30) return 35;
            return 50;
        }
        if(pers == AIPersonality.Traverser)
        {
            return Random.Range(25, 40);
        }

        return 50;
    }

    /// <summary>
    /// Determine how much money a player is wiliing to spend on an ore in the auction
    /// </summary>
    public static int GetBuyPrice(int playerIndex, TileType ore)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        int oreAmount = GameData.Instance.playerOreSupplies[playerIndex][ore];

        if (pers == AIPersonality.Basic)
        {
            if (oreAmount < 20)
                return 40;
            if (oreAmount < 40)
                return 30;
            if (oreAmount < 60)
                return 25;
            if (oreAmount < 80)
                return 20;
            return 15;
        }
        if (pers == AIPersonality.Traverser)
        {
            if (ore == TileType.Food) return Random.Range(35,48);
            return Random.Range(15,30);
        }

        return 15;
    }


    public static TileType GetTileTypeToSeek(int playerIndex, bool random)
    {
        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        Mine mine = GameData.Instance.playerMineLocations[playerIndex];
        int floor = 0;
        Tile[] currentMineLayout = new Tile[0] ;

        if (mine == Mine.IronMine)
        {
            floor = GameData.Instance.ironFloors[playerIndex];
        }

        if (mine == Mine.JellyMine)
        {
            floor = GameData.Instance.jellyFloors[playerIndex];
        }

        if (mine == Mine.CoalMine)
        {
            floor = GameData.Instance.coalFloors[playerIndex];
        }

        currentMineLayout = MineRecorder.GetMineFloor(mine, floor);
        Dictionary<TileType, int> tileKinds = new Dictionary<TileType, int>();
        List<TileType> preferences = AIPersonalityPreferences.GetTilePreferences(pers);
        for (int i = 0;i < currentMineLayout.Length; i++)
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
            foreach (KeyValuePair<TileType,int> entry in tileKinds)
            {
                if (index == rand)
                    return entry.Key;
                index++;
            }
        }
        if(pers == AIPersonality.Traverser)
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
            if(rand < tileTransforms.Count)
                return tileTransforms[rand];
        }
        //get closest
        if(pers == AIPersonality.Traverser)
        {
            float minDist = 100;
            Transform minTransform = null;
            for(int i = 0; i < tileTransforms.Count; i++)
            {
                if(Vector3.Distance(playerPos.position,tileTransforms[i].position) < minDist)
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
        if(pers == AIPersonality.Basic)
        {
            int rand = Random.Range(0, 3);
            if (rand == 0)
                return Mine.IronMine;
            else if(rand == 1)
                return Mine.JellyMine;
            return Mine.CoalMine;
        }
        if (pers == AIPersonality.Traverser)
            return Mine.JellyMine;

        return Mine.IronMine;

    }

    public static Transform ChooseElevator(int playerIndex, List<Collider2D> objs)
    {
        if (objs.Count == 0)
            return null;

        AIPersonality pers = GameData.Instance.AIs[playerIndex];
        if(pers == AIPersonality.Basic || pers == AIPersonality.Traverser) //go to deepest mine :) 
        {
            int maxFloor = 0;
            Transform t = null;
            for(int i = 0;i < objs.Count; i++)
            {
                if(objs[i].GetComponent<ElevatorObj>().floor > maxFloor)
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
        int currentFloor = 0;
        if (GameData.Instance.playerMineLocations[playerIndex] == Mine.IronMine)
            currentFloor = GameData.Instance.ironFloors[playerIndex];
        if (GameData.Instance.playerMineLocations[playerIndex] == Mine.JellyMine)
            currentFloor = GameData.Instance.jellyFloors[playerIndex];
        if (GameData.Instance.playerMineLocations[playerIndex] == Mine.CoalMine)
            currentFloor = GameData.Instance.coalFloors[playerIndex];

        int currentElevator = 0;
        for(int i = 0;i < GameData.Instance.playerElevators[playerIndex].Count; i++)
        {
            if (GameData.Instance.playerElevators[playerIndex][i].mineType == GameData.Instance.playerMineLocations[playerIndex])
                currentElevator = GameData.Instance.playerElevators[playerIndex][i].floor;
        }

        if (currentFloor <= currentElevator) return false;
        if (GameData.Instance.playerOreSupplies[playerIndex][TileType.Coal] < 20) return false;

        if(pers == AIPersonality.Basic || pers == AIPersonality.Traverser)
        {
            if (currentFloor - currentElevator >= 10 && Random.Range(0,3)>1) return true;
        }

        return false;
    }

}
