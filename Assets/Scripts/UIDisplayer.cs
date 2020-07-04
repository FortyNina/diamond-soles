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

    [SerializeField]
    private TextMeshProUGUI[] _moneyDisplay;


    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < _playerFloorDisplay.Length;i++)
        {
            _playerFloorDisplay[i].text = "Floor: " + GameData.Instance.ironFloors[i];
        }

        for(int i = 0; i < _ironCollectedDisplay.Length; i++)
        {
            _ironCollectedDisplay[i].text = "Iron ore: " + GameData.Instance.playerOreSupplies[i][TileType.Iron];
        }

        for (int i = 0; i < _jellyCollectedDisplay.Length; i++)
        {
            _jellyCollectedDisplay[i].text = "Food: " + GameData.Instance.playerOreSupplies[i][TileType.Jelly];
        }


        for(int i = 0; i < _moneyDisplay.Length;i++)
        {
            _moneyDisplay[i].text = "Money: $" + GameData.Instance.playerMoney[i];
        }
    }
}
