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
        
        _requirement.text = contract.requirementSynopsis;
        _reward.text = "Payment : $" + c.rewardMoney;
    }
}
