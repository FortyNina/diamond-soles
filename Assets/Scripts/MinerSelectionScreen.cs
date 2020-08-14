using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class MinerSelectionScreen : MonoBehaviour
{

    [SerializeField]
    private GameObject _minerDataPrefab;

    [SerializeField]
    private Transform _minerDataParent;

    [SerializeField]
    private TextMeshProUGUI _familyIronDisplay;

    [SerializeField]
    private TextMeshProUGUI _familyJellyDisplay;

    [SerializeField]
    private TextMeshProUGUI _familyCoalDisplay;

    private List<MinerAddedData> _minersAdded = new List<MinerAddedData>();

    public int maxMiners = 4;

    private void Update()
    {
        _familyIronDisplay.text = "Total Iron: " + GameData.Instance.familyOreSupplies[TileType.Iron];
        _familyJellyDisplay.text = "Total Food: " + GameData.Instance.familyOreSupplies[TileType.Food];
        _familyCoalDisplay.text = "Total Coal: " + GameData.Instance.familyOreSupplies[TileType.Coal];

    }



    public void AddMiner(string s)
    {
        if (_minersAdded.Count >= maxMiners)
            return;

        AIPersonality pers = AIPersonality.Basic;
        var values = Enum.GetValues(typeof(AIPersonality));
        foreach (AIPersonality v in values){
            if (s == v.ToString()) pers = v;
        }

        GameObject go = Instantiate(_minerDataPrefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = _minerDataParent;
        MinerAddedData d = go.GetComponent<MinerAddedData>();
        d.Personality = pers;
        d.selection = this;
        _minersAdded.Add(d);
        UpdateMinerDataIDs();
    }

    public void RemoveMiner(int id)
    {
        GameObject go = _minersAdded[id].gameObject;
        _minersAdded.RemoveAt(id);
        Destroy(go);
        UpdateMinerDataIDs();

    }

    private void UpdateMinerDataIDs()
    {
        for(int i = 0;i < _minersAdded.Count; i++)
        {
            _minersAdded[i].playerID = i;
        }
    }

    public void SaveDataAndLoadMines()
    {
        GameData.Instance.numPlayers = _minersAdded.Count;
        List<int> durability = new List<int>();
        List<float> energy = new List<float>();
        List<int> coal = new List<int>();
        List<AIPersonality> personalities = new List<AIPersonality>();

        for (int i = 0; i < _minersAdded.Count; i++)
        {
            durability.Add(_minersAdded[i].Durability);
            energy.Add(_minersAdded[i].Energy);
            coal.Add(_minersAdded[i].Coal);
            personalities.Add(_minersAdded[i].Personality);

        }

        GameData.Instance.durabilityLevels = durability;
        GameData.Instance.energyLevels = energy;
        GameData.Instance.coalLevels = coal;
        GameData.Instance.AIs = personalities;

        SceneManager.LoadScene("MiningPhase");

    }

}

