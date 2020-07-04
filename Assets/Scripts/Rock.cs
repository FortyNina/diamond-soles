using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    public TileType ore;
    public int oreAmount;
    public int health;

    private int _playerID;

    public GameObject holePrefab;


    public void CreateSettings(int playerID)
    {
        oreAmount = 10;
        health = 1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Axe")
        {
            health--;
            if(health <= 0)
            {
                if (GameData.Instance.playerOreSupplies[_playerID].ContainsKey(ore))
                {
                    GameData.Instance.playerOreSupplies[_playerID][ore] += oreAmount;

                }
                if (Random.Range(0, 10) < 1)
                {
                    GameObject go = Instantiate(holePrefab, transform.position, Quaternion.identity);
                    go.transform.parent = transform.parent;
                    go.transform.position = transform.position;
                }

                Destroy(gameObject);
            }
        }
    }
}
