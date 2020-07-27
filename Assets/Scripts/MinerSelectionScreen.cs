using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinerSelectionScreen : MonoBehaviour
{

    [SerializeField]
    private GameObject _minerDataPrefab;

    [SerializeField]
    private Transform _minerDataParent;

    private List<MinerAddedData> _minersAdded = new List<MinerAddedData>();

    public int maxMiners = 4;



    public void AddMiner()
    {
        if (_minersAdded.Count >= maxMiners)
            return;

        GameObject go = Instantiate(_minerDataPrefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = _minerDataParent;
        MinerAddedData d = go.GetComponent<MinerAddedData>();
        _minersAdded.Add(d);
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

