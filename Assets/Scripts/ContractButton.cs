using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContractButton : MonoBehaviour
{

    public bool taken = false;

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _requirement;
    [SerializeField] private TextMeshProUGUI _reward;

    public Contract contract;

    public void Init(Contract c)
    {
        contract = c;
        _title.text = contract.contractTitle;
        _description.text = contract.contractDescription;
        string req = "Needed: ";
        foreach(KeyValuePair<TileType,int> entry in c.requirements)
        {
            req += entry.Value + " " + entry.Key + " ";
        }
        _requirement.text = req;
        _reward.text = "Payment : $" + c.rewardMoney;
    }
}
