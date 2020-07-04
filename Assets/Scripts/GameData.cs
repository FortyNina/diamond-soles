using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
	protected GameData() { } // guarantee this will be always a singleton only - can't use the constructor!


	public List<PlayerController> players = new List<PlayerController>();

	public List<int> ironFloors = new List<int>();
	public List<int> jellyFloors = new List<int>();
	public List<int> thirdFloors = new List<int>();


	public List<Dictionary<TileType, int>> playerOreSupplies = new List<Dictionary<TileType, int>>();



}
