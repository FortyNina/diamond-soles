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

    [SerializeField]
    private TextMeshProUGUI _p1IronDisplay;

    [SerializeField]
    private TextMeshProUGUI _p2IronDisplay;

    [SerializeField]
    private TextMeshProUGUI _p3IronDisplay;

    [SerializeField]
    private TextMeshProUGUI _p1JellyDisplay;

    [SerializeField]
    private TextMeshProUGUI _p2JellyDisplay;

    [SerializeField]
    private TextMeshProUGUI _p3JellyDisplay;

    [SerializeField]
    private TextMeshProUGUI _p1CoalDisplay;

    [SerializeField]
    private TextMeshProUGUI _p2CoalDisplay;

    [SerializeField]
    private TextMeshProUGUI _p3CoalDisplay;

    [SerializeField]
    private TextMeshProUGUI _p1MoneyDisplay;

    [SerializeField]
    private TextMeshProUGUI _p2MoneyDisplay;

    [SerializeField]
    private TextMeshProUGUI _p3MoneyDisplay;



    // Update is called once per frame
    void Update()
    {
        _p1LevelDisplay.text = "Floor: " + GetFloor(GameData.Instance.playerMineLocations[1], 1).ToString();
        _p2LevelDisplay.text = "Floor: " + GetFloor(GameData.Instance.playerMineLocations[2], 2).ToString();
        _p3LevelDisplay.text = "Floor: " + GetFloor(GameData.Instance.playerMineLocations[3], 3).ToString();
        _p1IronDisplay.text = "Iron: " + GameData.Instance.playerOreSupplies[1][TileType.Iron];
        _p2IronDisplay.text = "Iron: " + GameData.Instance.playerOreSupplies[2][TileType.Iron];
        _p3IronDisplay.text = "Iron: " + GameData.Instance.playerOreSupplies[3][TileType.Iron];
        _p1JellyDisplay.text = "Food: " + GameData.Instance.playerOreSupplies[1][TileType.Food];
        _p2JellyDisplay.text = "Food: " + GameData.Instance.playerOreSupplies[2][TileType.Food];
        _p3JellyDisplay.text = "Food: " + GameData.Instance.playerOreSupplies[3][TileType.Food];
        _p1CoalDisplay.text = "Coal: " + GameData.Instance.playerOreSupplies[1][TileType.Coal];
        _p2CoalDisplay.text = "Coal: " + GameData.Instance.playerOreSupplies[2][TileType.Coal];
        _p3CoalDisplay.text = "Coal: " + GameData.Instance.playerOreSupplies[3][TileType.Coal];
        _p1MoneyDisplay.text = "Money: " + GameData.Instance.playerMoney[1];
        _p2MoneyDisplay.text = "Money: " + GameData.Instance.playerMoney[2];
        _p3MoneyDisplay.text = "Money: " + GameData.Instance.playerMoney[3];


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
            _jellyCollectedDisplay[i].text = "Food: " + GameData.Instance.playerOreSupplies[i][TileType.Food];
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
