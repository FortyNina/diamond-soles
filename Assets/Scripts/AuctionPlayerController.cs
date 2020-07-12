using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionPlayerController : MonoBehaviour
{

    public bool isBuyer;

    public int playerID;

    public int currentPrice;

    public bool isAI;

    private TileType currentOre;

    private int _max = 50;
    private int _min = 15;

    [SerializeField]
    private Transform _ceiling;

    [SerializeField]
    private Transform _floor;

    public KeyCode up;
    public KeyCode down;

    private int upperBound;
    private int lowerBound;


    private float _timer = 0;

    private int _AItargetPrice = 0;

   
    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;


       
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
                    _AItargetPrice = AIManager.GetBuyPrice(playerID, currentOre);
                else
                    _AItargetPrice = AIManager.GetSellPrice(playerID, currentOre);

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
        gameObject.transform.position = new Vector3(transform.position.x, newY, 0);


        
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



}
