using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController[] _playersInScene;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < _playersInScene.Length; i++)
        {
            GameData.Instance.players.Add(_playersInScene[i]);
            GameData.Instance.ironFloors.Add(0);
            GameData.Instance.jellyFloors.Add(0);
            GameData.Instance.thirdFloors.Add(0);

            Dictionary<TileType, int> ores = new Dictionary<TileType, int>();

            ores.Add(TileType.Iron, 0);
            ores.Add(TileType.Diamond, 0);
            ores.Add(TileType.Jelly, 0);
            ores.Add(TileType.Third, 0);

            GameData.Instance.playerOreSupplies.Add(ores);



        }
    }

}
