﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{

    [SerializeField]
    private Image _energyDisplay;

    [SerializeField]
    private Image _durabilityDisplay;

    [SerializeField]
    private TextMeshProUGUI _floorDisplay;

    [SerializeField]
    private TextMeshProUGUI _ironCollectedDisplay;

    [SerializeField]
    private TextMeshProUGUI _jellyCollectedDisplay;

    [SerializeField]
    private TextMeshProUGUI _coalCollectedDisplay;

    private int _focusedID = 0;

    // Update is called once per frame
    void Update()
    {
        _energyDisplay.fillAmount = (float)GameData.Instance.energyLevels[_focusedID] / GameSettings.Instance.maxEnergy;
        _durabilityDisplay.fillAmount = (float)GameData.Instance.durabilityLevels[_focusedID] / GameSettings.Instance.maxDurability;
        _floorDisplay.text = "Floor: #" + GameData.Instance.playerFloors[_focusedID][GameData.Instance.playerMineLocations[_focusedID]];
        _ironCollectedDisplay.text = GameData.Instance.playerOreSupplies[_focusedID][TileType.Iron].ToString();
        _jellyCollectedDisplay.text = GameData.Instance.playerOreSupplies[_focusedID][TileType.Food].ToString();
        _coalCollectedDisplay.text = GameData.Instance.playerOreSupplies[_focusedID][TileType.Coal].ToString();


    }

    public void UpdateFocusID(int newID)
    {
        _focusedID = newID;
    }




}
