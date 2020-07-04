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

        for(int i = 0; i < _energyDisplay.Length; i++)
        {
            _energyDisplay[i].fillAmount = (float)GameData.Instance.energyLevels[i] / 100f;
            Debug.Log((float)GameData.Instance.energyLevels[i] / 100f);
        }

        for (int i = 0; i < _durabilityDisplay.Length; i++)
        {
            _durabilityDisplay[i].fillAmount = (float)GameData.Instance.durabilityLevels[i] / 100f;
        }
    }
}
