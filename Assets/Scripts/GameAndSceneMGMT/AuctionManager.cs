using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AuctionManager : MonoBehaviour
{
    private enum AuctionState { Wait, BuyerSellerSelect, AuctionSetup, AuctionHappening, EndAuction }

    private TextMeshProUGUI _auctionMaterialTitle;

    [SerializeField] private GameObject _sellLine;

    [SerializeField] private GameObject _buyLine;

    [SerializeField] private Transform _ceilingLoc;

    [SerializeField] private Transform _floorLoc;


    private AuctionState _state = AuctionState.Wait;

    private AuctionPlayerController[] _players;

    private bool[] _buyerSellerSelected;
    private bool[] _isBuyer;

    private int _currentSellerPlayer = -1;
    private int _currentBuyerPlayer = -1;

    private int _currentSellMin = 0;
    private int _currentBuyMax = 0;

    private int _resourceIndex = 0;
    private TileType[] _resourcesToTrade = { TileType.Iron, TileType.Food };

    private float _transactionTimer = 1f;
    private float _auctionTimer = 40;



    public void BeginSequences(AuctionPlayerController[] players)
    {
        _players = players;
        _buyerSellerSelected = new bool[_players.Length]; 
        _isBuyer = new bool[_players.Length];
        _state = AuctionState.BuyerSellerSelect;

    }


    private void Update()
    {
        Debug.Log("State : " + _state.ToString());
        #region Wait
        if (_state == AuctionState.Wait)
        {
            //nothing
        }
        #endregion

        #region Buyer / Seller Select
        else if (_state == AuctionState.BuyerSellerSelect) //wait for everyone to choose if theyre a buyer or seller
        {
            bool allDone = true;

            for (int i = 0; i < _buyerSellerSelected.Length; i++)
            {
                if (!_buyerSellerSelected[i])
                    allDone = false;
            }

            if (allDone)
            {
                _state = AuctionState.Wait;
                StartCoroutine(ShowChoices());
            }

        }
        #endregion

        #region Setup Auction
        else if (_state == AuctionState.AuctionSetup)
        {
            _currentBuyMax = 15;
            _currentSellMin = 50;

            _sellLine.SetActive(true);
            _buyLine.SetActive(true);

            _sellLine.transform.position = _ceilingLoc.position;
            _buyLine.transform.position = _floorLoc.position;

            for (int i = 0; i < _players.Length; i++)
            {

                _players[i].SetOreType(_resourcesToTrade[_resourceIndex]);
                _players[i].isBuyer = _isBuyer[i];
                if (_players[i].isBuyer)
                {
                    _players[i].currentPrice = _currentBuyMax;
                }
                else
                {
                    _players[i].currentPrice = _currentSellMin;
                }
                _players[i].SetAuctionPhase();
            }

            _state = AuctionState.AuctionHappening;
        }



        #endregion

        #region Auction Phase
        else if (_state == AuctionState.AuctionHappening)
        {

            int minSell = SetSellLine();

            int maxBuy = SetBuyLine();

            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].isBuyer)
                    _players[i].SetNewBounds(15, minSell);
                else
                    _players[i].SetNewBounds(maxBuy, 50);

            }

//            _auctionMaterialTitle.text = "buyer: Player " + _currentBuyerPlayer + " seller: Player " + _currentSellerPlayer;


            //ok now see if we have a sale on our hands
            if (_transactionTimer < 0)
            {
                for (int i = 0; i < _players.Length; i++)
                {
                    if (_players[i].isBuyer)
                    {
                        //there is a buyer in our midst
                        if (_players[i].currentPrice == maxBuy && i == _currentBuyerPlayer && maxBuy == minSell)
                        {
                            GameData.Instance.playerMoney[i] -= maxBuy;
                            GameData.Instance.playerOreSupplies[i][_resourcesToTrade[_resourceIndex]] += 5;
                        }
                    }
                    else
                    {
                        //there is a seller in our midst
                        if (_players[i].currentPrice == minSell && i == _currentSellerPlayer && maxBuy == minSell)
                        {
                            GameData.Instance.playerMoney[i] += maxBuy;
                            GameData.Instance.playerOreSupplies[i][_resourcesToTrade[_resourceIndex]] -= 5;
                            
                        }
                    }




                }
                _transactionTimer = 1;
            }

            _transactionTimer -= Time.deltaTime;
            _auctionTimer -= Time.deltaTime;
            if (_auctionTimer < 0 || Input.GetKeyUp(KeyCode.P))
            {
                _resourceIndex++;
                if (_resourceIndex >= _resourcesToTrade.Length)
                    LeaveAuction();
                else
                    _state = AuctionState.EndAuction;


                _auctionTimer = 40;
            }





        }

        #endregion


        #region End of Auction

        else if(_state == AuctionState.EndAuction)
        {
            for (int i = 0; i < _players.Length; i++)
            {
                _players[i].SetBuyerSellerSelectPhase();
            }

            _sellLine.SetActive(false);
            _buyLine.SetActive(false);

           
            _state = AuctionState.BuyerSellerSelect;

        }


        #endregion
    }



    private int SetSellLine()
    {
        //find out where the sell line should be 
        int minSell = 50;
        int playerIndex = -1;
        for (int i = 0; i < _players.Length; i++)
        {
            if (!_players[i].isBuyer)
            {
                if (_players[i].currentPrice < minSell)
                {
                    minSell = _players[i].currentPrice;
                    playerIndex = i;
                }

            }
        }

        for (int i = 0; i < _players.Length; i++)
        {
            if (i == _currentSellerPlayer && _players[i].currentPrice != minSell)
            {
                _currentSellerPlayer = playerIndex;
            }

        }
        if (minSell != _currentSellMin)
        {
            _currentSellMin = minSell;
            _currentSellerPlayer = playerIndex;
        }


        float pricePercent = (minSell - 15f) / (50 - 15);
        float newY = Mathf.Lerp(_floorLoc.position.y, _ceilingLoc.position.y, pricePercent);
        _sellLine.transform.position = new Vector3(_sellLine.transform.position.x, newY, 0);
        _sellLine.transform.Find("Num").GetComponent<TextMeshProUGUI>().text = minSell.ToString();

        return minSell;
    }


    private int SetBuyLine()
    {

        int maxBuy = 15;
        int playerIndex = -1;
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i].isBuyer)
            {
                if (_players[i].currentPrice > maxBuy)
                {
                    maxBuy = _players[i].currentPrice;
                    playerIndex = i;
                }

            }
        }

        for (int i = 0; i < _players.Length; i++)
        {
            if (i == _currentBuyerPlayer && _players[i].currentPrice != maxBuy)
            {
                _currentBuyerPlayer = playerIndex;
            }

        }
        if (maxBuy != _currentBuyMax)
        {
            _currentBuyMax = maxBuy;
            _currentBuyerPlayer = playerIndex;
        }




        float pricePercent = (maxBuy - 15f) / (50 - 15);
        float newY = Mathf.Lerp(_floorLoc.position.y, _ceilingLoc.position.y, pricePercent);
        _buyLine.transform.position = new Vector3(_buyLine.transform.position.x, newY, 0);
        _buyLine.transform.Find("Num").GetComponent<TextMeshProUGUI>().text = maxBuy.ToString();

        return maxBuy;

    }

    public IEnumerator ShowChoices()
    {
        yield return new WaitForSeconds(5);
  
        _state = AuctionState.AuctionSetup;

    }

    public void BuyerSelectionMade(int index)
    {
        _buyerSellerSelected[index] = true;
        _isBuyer[index] = true;
    }

    public void SellerSelectionMade(int index)
    {
        _buyerSellerSelected[index] = true;
        _isBuyer[index] = false;

    }

    public void LeaveAuction()
    {
        //drop floors?
        for (int i = 0; i < GameData.Instance.playerFloors.Count; i++)
        {
            GameData.Instance.playerFloors[i][Mine.IronMine] = 0;
            GameData.Instance.playerFloors[i][Mine.JellyMine] = 0;
            GameData.Instance.playerFloors[i][Mine.CoalMine] = 0;

        }


        SceneManager.LoadScene("MiningPhase");
    }


}
