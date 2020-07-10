using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
	protected GameData() { } // guarantee this will be always a singleton only - can't use the constructor!

	public bool setUpComplete = false;

	public List<AIPersonality> AIs = new List<AIPersonality>();

	public List<int> ironFloors = new List<int>();
	public List<int> jellyFloors = new List<int>();
	public List<int> thirdFloors = new List<int>();


	public List<Dictionary<TileType, int>> playerOreSupplies = new List<Dictionary<TileType, int>>();

	public List<float> energyLevels = new List<float>();
	public List<int> durabilityLevels = new List<int>();
	public List<int> thirdLevels = new List<int>();


	public List<int> playerMoney = new List<int>();

	public int numberRealPlayers = 1;


	//TODO: dont hardcode number of values!
	public List<Vector3> playerLocalLocations = new List<Vector3> { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, };

	public List<Mine> playerMineLocations = new List<Mine> { Mine.IronMine, Mine.IronMine, Mine.IronMine, Mine.IronMine };



}
