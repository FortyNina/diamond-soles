using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionPlayerController : MonoBehaviour
{

    public bool isBuyer;

    public int playerID;

    public int currentPrice;

    private int _max = 50;
    private int _min = 15;

    [SerializeField]
    private Transform _ceiling;

    [SerializeField]
    private Transform _floor;

    public KeyCode up;
    public KeyCode down;


    private float _timer = 0;

   
    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;



        Debug.Log("price: " + currentPrice + " " +  gameObject.name);

        if(_timer< 0)
        {
            if (Input.GetKey(up) && currentPrice < _max)
            {
                currentPrice++;
            }
            if (Input.GetKey(down) && currentPrice > _min)
            {
                currentPrice--;
            }

            _timer = .1f;
        }

        if (Input.GetKeyUp(up) && currentPrice < _max)
        {
            currentPrice++;
        }
        if (Input.GetKeyDown(down) && currentPrice > _min)
        {
            currentPrice--;
        }

        float pricePercent = (currentPrice - (float)_min) / (_max - (float)_min);
        float newY = Mathf.Lerp(_floor.position.y, _ceiling.position.y, pricePercent);
        gameObject.transform.position = new Vector3(transform.position.x, newY, 0);


        
    }

    public void SetNewBounds(int min, int max)
    {
        _max = max;
        _min = min;
    }

}
