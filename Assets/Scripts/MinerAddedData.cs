using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinerAddedData : MonoBehaviour
{

    public int playerID;

    [SerializeField]
    private TextMeshProUGUI nameText;

    private int _durability;
    private int _energy;
    private int _coal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = "Miner " + playerID;
    }
}
