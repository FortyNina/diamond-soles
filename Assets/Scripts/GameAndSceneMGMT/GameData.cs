using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{

	public int numPlayers = 4;
	public int numberRealPlayers = 0;
	public int numAuctionAi = 3;


	protected GameData() { } // guarantee this will be always a singleton only - can't use the constructor!

	public bool setUpComplete = false;

	public List<AIPersonality> AIs = new List<AIPersonality>();
	public List<AIAuctionPersonality> auctionAIs = new List<AIAuctionPersonality>();

	
	public List<Dictionary<Mine, int>> playerFloors = new List<Dictionary<Mine, int>>();


	public List<Dictionary<TileType, int>> playerOreSupplies = new List<Dictionary<TileType, int>>();
	public Dictionary<TileType, int> familyOreSupplies = new Dictionary<TileType, int>();
	public List<Dictionary<TileType, int>> auctionAIOreSupplies = new List<Dictionary<TileType, int>>();

	public List<float> energyLevels = new List<float>();
	public List<int> durabilityLevels = new List<int>();
	public List<int> coalLevels = new List<int>();


	public List<int> playerMoney = new List<int>();
	public List<int> auctionAIMoney = new List<int>();

	public int familyMoney = 1000;


	public List<Vector3> playerLocalLocations = new List<Vector3> ();

	public List<Mine> playerMineLocations = new List<Mine> ();

	public List<List<ElevatorData>> playerElevators = new List<List<ElevatorData>>();

	public List<Vector3> gridLocations = new List<Vector3>();

	public int playerInFocus = 0;



}
