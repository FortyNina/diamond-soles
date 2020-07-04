using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDisplayer : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] _playerFloorDisplay;

    [SerializeField]
    private TextMeshProUGUI[] _ironCollectedDisplay;

    [SerializeField]
    private TextMeshProUGUI[] _jellyCollectedDisplay;


    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < _playerFloorDisplay.Length;i++)
        {
            _playerFloorDisplay[i].text = "Floor: " + GameData.Instance.ironFloors[i];
        }

        for(int i = 0; i < _ironCollectedDisplay.Length; i++)
        {
            _ironCollectedDisplay[i].text = "Iron ore: " + GameData.Instance.playerOreSupplies[0][TileType.Iron];
        }

        for (int i = 0; i < _jellyCollectedDisplay.Length; i++)
        {
            _jellyCollectedDisplay[i].text = "Food: " + GameData.Instance.playerOreSupplies[0][TileType.Jelly];
        }
    }
}
