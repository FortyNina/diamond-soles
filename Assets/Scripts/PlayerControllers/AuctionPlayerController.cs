using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AuctionPlayerController : MonoBehaviour
{

    public KeyCode up;
    public KeyCode down;

    public AuctionManager am;
    
    [SerializeField] private GameObject _playerGroup;
    [SerializeField] private GameObject _buyerSellerSelectGroup;
    [SerializeField] private TextMeshProUGUI _decisionText;
    [SerializeField] private TextMeshProUGUI _unitsText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private GameObject _playerObj;

    [HideInInspector] public bool isBuyer;
    [HideInInspector] public int playerID;
    [HideInInspector] public int currentPrice;
    [HideInInspector] public bool isAI;



    private TileType currentOre;

    private int _max = 50;
    private int _min = 15;

    [HideInInspector]
    public Transform _ceiling;

    [HideInInspector]
    public Transform _floor;



    private int upperBound;
    private int lowerBound;


    private float _timer = 0;

    private int _AItargetPrice = 0;

   
    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        _moneyText.text = "Money: " + Money();
        _unitsText.text = currentOre + ": " + Ore(currentOre);


       
        if (!isAI)
        {
            if (Input.GetKeyUp(up) && currentPrice < _max && currentPrice < upperBound)
            {
                currentPrice++;
            }
            if (Input.GetKeyDown(down) && currentPrice > _min && currentPrice > lowerBound)
            {
                currentPrice--;
            }
        }

        else
        {
            //do AI movement!
            if (_timer < 0)
            {
                if (isBuyer)
                    _AItargetPrice = AIAuctionManager.GetBuyPrice(playerID-1, currentOre);
                else
                    _AItargetPrice = AIAuctionManager.GetSellPrice(playerID-1, currentOre);

                if (currentPrice > _AItargetPrice)
                    currentPrice--;
                else if (currentPrice < _AItargetPrice)
                    currentPrice++;
            }

        }


        if (_timer < 0)
        {
            _timer = .1f;
        }


        float pricePercent = (currentPrice - (float)_min) / (_max - (float)_min);
        float newY = Mathf.Lerp(_floor.position.y, _ceiling.position.y, pricePercent);
        _playerObj.transform.position = new Vector3(_playerObj.transform.position.x, newY, 0);


        
    }

    public void SetNewBounds(int min, int max)
    {
        lowerBound = min;
        upperBound = max;
    }

    public void SetOreType(TileType ore)
    {
        currentOre = ore;
    }

    public void SetAuctionPhase()
    {
        _buyerSellerSelectGroup.SetActive(false);
        _playerGroup.SetActive(true);
    }

    public void SetBuyerSellerSelectPhase()
    {
        _buyerSellerSelectGroup.SetActive(true);
        _decisionText.gameObject.SetActive(false);
        _playerGroup.SetActive(false);

    }

    public void ClickBuyer()
    {
        am.SellerSelectionMade(playerID);
        _decisionText.text = "Buyer";
    }

    public void ClickSeller()
    {
        am.BuyerSelectionMade(playerID);
        _decisionText.text = "Seller";

    }

    private int Money()
    {
        if (isAI) return GameData.Instance.auctionAIMoney[playerID - 1];
        return GameData.Instance.familyMoney;
    }

    private int Ore(TileType t)
    {
        if (isAI) return GameData.Instance.auctionAIOreSupplies[playerID - 1][t];
        return GameData.Instance.familyOreSupplies[t];
    }



}
