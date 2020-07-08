using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AuctionManager : MonoBehaviour
{

    private enum AuctionState { BuyerSellerSetup, BuyerSellerWait, AuctionSetup, AuctionHappening, Wait}

    [SerializeField]
    private GameObject[] _buyerSellerSelectGroups;

    [SerializeField]
    private TextMeshProUGUI[] _buyerSellerChoiceDisplays;

    [SerializeField]
    private TextMeshProUGUI[] _moneyDisplay;

    [SerializeField]
    private TextMeshProUGUI[] _oreAmountDisplay;

    [SerializeField]
    private TextMeshProUGUI _auctionMaterialTitle;

    [SerializeField]
    private GameObject[] _playerObjects;

    [SerializeField]
    private GameObject _sellLine;

    [SerializeField]
    private GameObject _buyLine;

    [SerializeField]
    private Transform _ceilingLoc;

    [SerializeField]
    private Transform _floorLoc;

    private int ceiling = 50;
    private int floor = 15;

    private int currentSellMin = 0;
    private int currentBuyMax = 0;

    private bool[] _buyerSellerSelected;
    private bool[] _isBuyer;
    private AuctionState state;
    private int _resourceIndex = 0;
    private TileType[] _resourcesToTrade = { TileType.Iron };

    
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
            //Create displays
            for(int i = 0; i < _buyerSellerSelectGroups.Length; i++)
            {
                _buyerSellerSelectGroups[i].SetActive(true);
            }

            for(int i = 0; i < _moneyDisplay.Length; i++)
            {
                _moneyDisplay[i].text = "Money: $" + GameData.Instance.playerMoney[i];
            }

            for (int i = 0; i < _oreAmountDisplay.Length; i++)
            {
                _oreAmountDisplay[i].text = _resourcesToTrade[_resourceIndex].ToString() + " " + GameData.Instance.playerOreSupplies[i][_resourcesToTrade[_resourceIndex]];
            }

            for (int i = GameData.Instance.numberRealPlayers; i < _buyerSellerSelectGroups.Length; i++)
            {
                StartCoroutine(AIBuyerSellerDecisions(i, _resourcesToTrade[_resourceIndex]));
            }

            _auctionMaterialTitle.text = "current auction: " + _resourcesToTrade[_resourceIndex].ToString();

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
                state = AuctionState.Wait;
                StartCoroutine(ShowChoices());
            }
        }
        else if(state == AuctionState.AuctionSetup)
        {

            //three scenarios: everyone's a buyer, everyone's a seller, mixture
            int scenario = 0;
            for(int i = 0; i < _isBuyer.Length; i++)
            {
                if (_isBuyer[i])
                    scenario++;
            }

            if(scenario == 0) //EVERYONE IS A SELLER!
            {

            }

            else if(scenario >= _isBuyer.Length) //EVERYONE IS A BUYER
            {
                
            }

            else //WE GOT A MIX ON OUR HANDS
            {

            }

            currentBuyMax = 15;
            currentSellMin = 50;

            _sellLine.SetActive(true);
            _buyLine.SetActive(true);

            _sellLine.transform.position = _ceilingLoc.position;
            _buyLine.transform.position = _floorLoc.position;


            for (int i = 0; i < _playerObjects.Length; i++)
            {
                AuctionPlayerController apc = _playerObjects[i].GetComponent<AuctionPlayerController>();
                apc.isBuyer = _isBuyer[i];
                if (apc.isBuyer)
                {
                    apc.currentPrice = currentBuyMax;
                }
                else
                {
                    apc.currentPrice = currentSellMin;
                }
                _playerObjects[i].SetActive(true);
            }

            state = AuctionState.AuctionHappening;



        }

        else if(state  == AuctionState.AuctionHappening)
        {

            //find out where the sell line should b
            int minSell = 100;
            int playerIndex = -1;
            for(int i = 0; i < _playerObjects.Length; i++)
            {
                AuctionPlayerController apc = _playerObjects[i].GetComponent<AuctionPlayerController>();
                if (!apc.isBuyer)
                {
                    if(apc.currentPrice < minSell) {
                        minSell = apc.currentPrice;
                        playerIndex = i;
                    }

                }
            }

            float pricePercent = (minSell - 15f) / (50 -15);
            float newY = Mathf.Lerp(_floorLoc.position.y, _ceilingLoc.position.y, pricePercent);
            _sellLine.transform.position = new Vector3(_sellLine.transform.position.x, newY, 0);





            int maxBuy = 0;
            playerIndex = -1;
            for (int i = 0; i < _playerObjects.Length; i++)
            {
                AuctionPlayerController apc = _playerObjects[i].GetComponent<AuctionPlayerController>();
                if (apc.isBuyer)
                {
                    if (apc.currentPrice > maxBuy)
                    {
                        maxBuy = apc.currentPrice;
                        playerIndex = i;
                    }

                }
            }


            pricePercent = (maxBuy - 15f) / (50 - 15);
            newY = Mathf.Lerp(_floorLoc.position.y, _ceilingLoc.position.y, pricePercent);
            _buyLine.transform.position = new Vector3(_buyLine.transform.position.x, newY, 0);




        }

        else if(state == AuctionState.Wait)
        {

            //do nothing! just wait xD
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
        state = AuctionState.AuctionSetup;

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
