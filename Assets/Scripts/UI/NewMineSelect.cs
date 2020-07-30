using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewMineSelect : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown _menu;


    // Start is called before the first frame update
    void Start()
    {
        _menu = GetComponent<TMP_Dropdown>();
        UpdateDefaultSelection();
    }


    public void UpdateDefaultSelection()
    {
        if (GameData.Instance.playerMineLocations[GameData.Instance.playerInFocus] == Mine.IronMine) _menu.value = 0;
        if (GameData.Instance.playerMineLocations[GameData.Instance.playerInFocus] == Mine.JellyMine) _menu.value = 1;
        if (GameData.Instance.playerMineLocations[GameData.Instance.playerInFocus] == Mine.CoalMine) _menu.value = 2;

    }

    public void ChangeMine()
    {

        Mine newMine = Mine.Entry;
        if (_menu.value == 0) newMine = Mine.IronMine;
        if (_menu.value == 1) newMine = Mine.JellyMine;
        if (_menu.value == 2) newMine = Mine.CoalMine;

        if (newMine == GameData.Instance.playerMineLocations[GameData.Instance.playerInFocus]) return;

        GameData.Instance.playerMineLocations[GameData.Instance.playerInFocus] = newMine;
        GameData.Instance.playerFloors[GameData.Instance.playerInFocus][newMine] = 0;

    }



}
