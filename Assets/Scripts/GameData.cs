using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{

	public int numPlayers = 4;
	public int numberRealPlayers = 1;


	protected GameData() { } // guarantee this will be always a singleton only - can't use the constructor!

	public bool setUpComplete = false;

	public List<AIPersonality> AIs = new List<AIPersonality>();

	public List<int> ironFloors = new List<int>();
	public List<int> jellyFloors = new List<int>();
	public List<int> coalFloors = new List<int>();


	public List<Dictionary<TileType, int>> playerOreSupplies = new List<Dictionary<TileType, int>>();

	public List<float> energyLevels = new List<float>();
	public List<int> durabilityLevels = new List<int>();
	public List<int> coalLevels = new List<int>();


	public List<int> playerMoney = new List<int>();

	public List<Vector3> playerLocalLocations = new List<Vector3> ();

	public List<Mine> playerMineLocations = new List<Mine> ();

	public List<List<ElevatorData>> playerElevators = new List<List<ElevatorData>>();



}
