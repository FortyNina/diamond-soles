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

    [SerializeField]
    private TextMeshProUGUI _p1LevelDisplay;

    [SerializeField]
    private TextMeshProUGUI _p2LevelDisplay;

    [SerializeField]
    private TextMeshProUGUI _p3LevelDisplay;



    // Update is called once per frame
    void Update()
    {
        _p1LevelDisplay.text = "Floor: " + GetFloor(GameData.Instance.playerMineLocations[1], 1).ToString();
        _p2LevelDisplay.text = "Floor: " + GetFloor(GameData.Instance.playerMineLocations[2], 2).ToString();
        _p3LevelDisplay.text = "Floor: " + GetFloor(GameData.Instance.playerMineLocations[3], 3).ToString();
        for (int i = 0;i < _playerFloorDisplay.Length;i++)
        {
             _playerFloorDisplay[i].text = "Floor: " + GameData.Instance.playerFloors[i][GameData.Instance.playerMineLocations[i]];
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

    private int GetFloor(Mine mt, int id)
    {
        Debug.Log("whyyyy");
        return GameData.Instance.playerFloors[id][mt];
    }
}
