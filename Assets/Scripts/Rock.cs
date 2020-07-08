using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    public TileType ore;
    public int oreAmount;
    public int health;

    [HideInInspector]
    public Tile myTile;

    private int _playerID;

    public GameObject holePrefab;

    private int floorNumber;
    private Mine mineType;
    private int index;


    public void CreateSettings(int ore, int hea, int floor, Mine mine, int index, Tile tile)
    {
        oreAmount = ore;
        health = hea;
        floorNumber = floor;
        mineType = mine;
        myTile = tile;
        this.index = index;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Axe")
        {
            MineRecorder.UpdateMineTileHealth(floorNumber, mineType, -1, index);
            if(MineRecorder.GetMineTileHealth(floorNumber, mineType, index) <= 0)
            {
                if (GameData.Instance.playerOreSupplies[collision.transform.parent.GetComponent<GridCreator>().playerID].ContainsKey(ore))
                {
                    GameData.Instance.playerOreSupplies[collision.transform.parent.GetComponent<GridCreator>().playerID][ore] += myTile.oreAmount;

                }
                if (Random.Range(0, 10) < 1)
                {
                    GameObject go = Instantiate(holePrefab, transform.position, Quaternion.identity);
                    go.transform.parent = transform.parent;
                    go.transform.position = transform.position;
                    MineRecorder.UpdateTileType(floorNumber, index, mineType, TileType.Hole);
                }
                else
                {
                    MineRecorder.UpdateTileType(floorNumber, index, mineType, TileType.Blank);

                }
            }
        }
    }

   
}
