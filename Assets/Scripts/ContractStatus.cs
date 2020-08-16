using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContractStatus : MonoBehaviour
{

    public int  playerID;

    [SerializeField] private TextMeshProUGUI _contractStatusTitle;
    [SerializeField] private TextMeshProUGUI _contractDesc;
    [SerializeField] private GameObject _buttons;

    public  void Init()
    {
        if(playerID != 0) //do AI stuff
        {
            StartCoroutine(AiSelectContractSubmission());
        }
        _contractDesc.text = "Need " + GameData.Instance.companies[playerID].contract.requirementSynopsis;
    }


    public void SubmitContract()
    {
        _contractStatusTitle.gameObject.SetActive(true);
        _contractStatusTitle.text = "Contract Fulfilled";
        GameData.Instance.companies[playerID].FulfillContract();
        _buttons.SetActive(false);
    }

    public void DeclineContract()
    {
        _contractStatusTitle.gameObject.SetActive(true);
        _contractStatusTitle.text = "Contract Not Fulfilled";
        GameData.Instance.companies[playerID].DeclineContract();
        _buttons.SetActive(false);


    }

    private IEnumerator AiSelectContractSubmission()
    {
        //could probably move this to auctionAImanagaer
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        bool hasEnough = true;
        foreach(KeyValuePair<TileType, int> entry in GameData.Instance.companies[playerID].contract.requirements)
        {
            if (entry.Value > GameData.Instance.companies[playerID].oreSupplies[entry.Key]) hasEnough = false;
        }
        if (hasEnough) SubmitContract();
        else DeclineContract();
    }


}
