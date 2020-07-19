using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplayer : MonoBehaviour
{
    

    [SerializeField]
    private TextMeshProUGUI[] _playerFloorDisplay;

    [SerializeField]
    private TextMeshProUGUI[] _ironCollectedDisplay;

    [SerializeField]
    private TextMeshProUGUI[] _jellyCollectedDisplay;

    [SerializeField]
    private TextMeshProUGUI[] _coalCollectedDisplay;

    [SerializeField]
    private TextMeshProUGUI[] _moneyDisplay;

    [SerializeField]
    private Image[] _energyDisplay;

    [SerializeField]
    private Image[] _durabilityDisplay;


    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < _playerFloorDisplay.Length;i++)
        {
            if(GameData.Instance.playerMineLocations[i] == Mine.IronMine)
                _playerFloorDisplay[i].text = "Floor: " + GameData.Instance.ironFloors[i];
            if (GameData.Instance.playerMineLocations[i] == Mine.JellyMine)
                _playerFloorDisplay[i].text = "Floor: " + GameData.Instance.jellyFloors[i];
            if (GameData.Instance.playerMineLocations[i] == Mine.CoalMine)
                _playerFloorDisplay[i].text = "Floor: " + GameData.Instance.coalFloors[i];

        }

        for(int i = 0; i < _ironCollectedDisplay.Length; i++)
        {
            _ironCollectedDisplay[i].text = "Iron: " + GameData.Instance.playerOreSupplies[i][TileType.Iron];
        }

        for (int i = 0; i < _jellyCollectedDisplay.Length; i++)
        {
            _jellyCollectedDisplay[i].text = "Jelly: " + GameData.Instance.playerOreSupplies[i][TileType.Food];
        }

        for(int i = 0;i < _coalCollectedDisplay.Length; i++)
        {
            _coalCollectedDisplay[i].text = "Coal: " + GameData.Instance.playerOreSupplies[i][TileType.Coal];
        }


        for(int i = 0; i < _moneyDisplay.Length;i++)
        {
            _moneyDisplay[i].text = "Money: $" + GameData.Instance.playerMoney[i];
        }

        for(int i = 0; i < _energyDisplay.Length; i++)
        {
            _energyDisplay[i].fillAmount = (float)GameData.Instance.energyLevels[i] / 100f;
        }

        for (int i = 0; i < _durabilityDisplay.Length; i++)
        {
            _durabilityDisplay[i].fillAmount = (float)GameData.Instance.durabilityLevels[i] / 100f;
        }
    }
}
