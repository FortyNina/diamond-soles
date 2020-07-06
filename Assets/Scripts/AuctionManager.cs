using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AuctionManager : MonoBehaviour
{

    private enum AuctionState { BuyerSellerSetup, BuyerSellerWait, BuyerSellerChoiceDisplays}

    [SerializeField]
    private GameObject[] _buyerSellerSelectGroups;
    [SerializeField]
    private TextMeshProUGUI[] _buyerSellerChoiceDisplays;

    private bool[] _buyerSellerSelected;

    private bool[] _isBuyer;

    private AuctionState state;


    // Start is called before the first frame update
    void Start()
    {
        _buyerSellerSelected = new bool[_buyerSellerSelectGroups.Length];
        _isBuyer = new bool[_buyerSellerSelectGroups.Length];
        state = AuctionState.BuyerSellerSetup;
    }

    private void Update()
    {
        if(state == AuctionState.BuyerSellerSetup)
        {
            for(int i = 0; i < _buyerSellerSelectGroups.Length; i++)
            {
                _buyerSellerSelectGroups[i].SetActive(true);
            }

            for (int i = GameData.Instance.numberRealPlayers; i < _buyerSellerSelectGroups.Length; i++){
                StartCoroutine(AIBuyerSellerDecisions(i, TileType.Iron));
            }


            state = AuctionState.BuyerSellerWait;
        }


        else if(state == AuctionState.BuyerSellerWait)
        {
            bool allDone = true;
            for(int i =0;i< _buyerSellerSelected.Length; i++)
            {
                if(!_buyerSellerSelected[i])
                    allDone = false;
            }

            if (allDone)
            {
                state = AuctionState.BuyerSellerChoiceDisplays;
            }
        }

        else if(state == AuctionState.BuyerSellerChoiceDisplays)
        {

            //do nothing! just wait :)
        }






    }








    public void BuyerSelectionMade(int index)
    {
        _buyerSellerSelectGroups[index].SetActive(false);
        _buyerSellerSelected[index] = true;
        _isBuyer[index] = true;
        _buyerSellerChoiceDisplays[index].text = "Buyer";
    }

    public void SellerSelectionMade(int index)
    {
        _buyerSellerSelectGroups[index].SetActive(false);
        _buyerSellerSelected[index] = true;
        _isBuyer[index] = false;
        _buyerSellerChoiceDisplays[index].text = "Seller";


    }

    public IEnumerator ShowChoices()
    {
        yield return new WaitForSeconds(5);
        for (int i = 0; i < _buyerSellerChoiceDisplays.Length; i++)
            _buyerSellerChoiceDisplays[i].text = "";
        state = AuctionState.BuyerSellerSetup;
        //TODO: change to the auction selling part?

    }





    public IEnumerator AIBuyerSellerDecisions(int index, TileType oreType)
    {
        yield return new WaitForSeconds((float)Random.Range(.75f, 3.2f));
        if (AIManager.DetermineBuyer(index, oreType))
        {
            BuyerSelectionMade(index);
        }
        else
        {
            SellerSelectionMade(index);
        }
    }

    

    
}
