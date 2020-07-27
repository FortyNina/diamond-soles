using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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



    public void AddMiner()
    {
        if (_minersAdded.Count >= maxMiners)
            return;

        GameObject go = Instantiate(_minerDataPrefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = _minerDataParent;
        MinerAddedData d = go.GetComponent<MinerAddedData>();
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
    
}

