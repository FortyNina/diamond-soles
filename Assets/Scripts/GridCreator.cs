using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mine { IronMine, JellyMine, ThirdMine}

public class GridCreator : MonoBehaviour
{

    public Color blank;
    public Color spawn;
    public Color rock;
    public Color stair;

    public int playerID;

    public Mine mineType;

    [SerializeField]
    private int _gridWidth = 9;

    [SerializeField]
    private int _gridHeight = 9;

    [SerializeField]
    private float _tileWidth = .64f;

    [Space(9)]

    [SerializeField]
    private GameObject _rockTile;

    [SerializeField]
    private GameObject _blankTile;

    [SerializeField]
    private GameObject _stairTile;

    [SerializeField]
    private GameObject _ironTile;

    [SerializeField]
    private GameObject _jellyTile;


    [Space(9)]
    [SerializeField]
    private GameObject _playerObject;


    private Tile[] _tiles;

    private float _adjustedX;
    private float _adjustedY;





    // Start is called before the first frame update
    void Start()
    {
        _adjustedX = (-1 * ((_gridWidth * _tileWidth) / 2f) + _tileWidth / 2) + transform.position.x;
        _adjustedY = (-1 * ((_gridHeight * _tileWidth) / 2f) + _tileWidth / 2) + transform.position.y;

        CreateNewLayout();


    }

    public void CreateNewLayout()
    {
        DestroyCurrentTiles();
        _tiles = CreateRandomMine();
        DrawGrid();
    }

    private void DrawGrid()
    {
        float x = _adjustedX;
        float y = _adjustedY;

        //Create Blanks
        for (int i = 0; i < _tiles.Length; i += _gridWidth)
        {
            for (int j = 0; j < _gridWidth; j++)
            {
                Tile currentTile = _tiles[i + j];
                GameObject prefabToGenerate = _blankTile;

                GameObject go = Instantiate(prefabToGenerate, new Vector3(0, 0, 0), Quaternion.identity);
                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();

                go.transform.parent = transform;
                go.transform.position = new Vector3(x, y, 0);


                if (currentTile.TileType == TileType.Spawn)
                    _playerObject.transform.position = new Vector3(x, y, 0);

                x += _tileWidth;
            }
            x = _adjustedX;
            y += _tileWidth;
        }

        x = _adjustedX;
        y = _adjustedY;

        //Create other objects
        for (int i = 0; i< _tiles.Length; i+= _gridWidth)
        {
            for (int j = 0; j < _gridWidth;j++)
            {
                Tile currentTile = _tiles[i + j];
                GameObject prefabToGenerate = _blankTile;

                if (currentTile.TileType == TileType.Rock)
                    prefabToGenerate = _rockTile;
                else if (currentTile.TileType == TileType.Stair)
                    prefabToGenerate = _stairTile;
                else if (currentTile.TileType == TileType.Iron)
                    prefabToGenerate = _ironTile;
                else if (currentTile.TileType == TileType.Jelly)
                    prefabToGenerate = _jellyTile;

                if (prefabToGenerate != _blankTile)
                {

                    GameObject go = Instantiate(prefabToGenerate, new Vector3(0, 0, 0), Quaternion.identity);
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();



                    go.transform.parent = transform;
                    go.transform.position = new Vector3(x, y, 0);


                    if (currentTile.TileType == TileType.Spawn)
                        _playerObject.transform.position = new Vector3(x, y, 0);

                }


                x += _tileWidth;
            }
            x = _adjustedX;
            y += _tileWidth;
        }
    }

    private void DestroyCurrentTiles()
    {
        foreach (Transform child in transform)
        {
            if(child.tag != "Player" && child.tag != "Axe")
               Destroy(child.gameObject);
        }
    }

    private Tile GetRandomTile(int blank, int basic, int iron, int jelly, int third)
    {
        int rand = Random.Range(0, 100);
        int marker = 0;

        for (int i = marker; i < blank + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Blank, playerID);
        }
        marker += blank;
        for (int i = marker; i < basic + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Rock, playerID);
        }
        marker += basic;
        for (int i = marker; i < iron + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Iron, playerID);
        }
        marker += iron;
        for (int i = marker; i < jelly + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Jelly, playerID);
        }
        marker += jelly;
        for (int i = marker; i < third + marker; i++)
        {
            if (rand == i)
                return new Tile(TileType.Third, playerID);
        }
        marker += third;



        return new Tile(TileType.Blank, playerID);
        
    }

    private Tile[] CreateRandomMine()
    {
        int floor = GameData.Instance.ironFloors[playerID];

        int percentBlank = 0;
        int percentBasic = 0;
        int percentIron = 0;
        int percentJelly = 0;
        int percentThird = 0;

        if (mineType == Mine.IronMine)
        {
            if (floor < 10)
            {
                percentBlank = 90;
                percentBasic = 7;
                percentIron = 3;
            }
            else if (floor < 20)
            {
                percentBlank = 80;
                percentBasic = 13;
                percentIron = 7;
            }
            else if (floor < 30)
            {
                percentBlank = 80;
                percentBasic = 9;
                percentIron = 11;
            }
        }

        else if(mineType == Mine.JellyMine)
        {
            if (floor < 10)
            {
                percentBlank = 90;
                percentBasic = 7;
                percentJelly = 3;
            }
            else if (floor < 20)
            {
                percentBlank = 80;
                percentBasic = 13;
                percentJelly = 7;
            }
            else if (floor < 30)
            {
                percentBlank = 80;
                percentBasic = 9;
                percentJelly = 11;
            }
        }

        //-----------------------------------------------------------------------------------------------------

        Tile[] newTiles = new Tile[_gridWidth * _gridHeight];

        for (int i = 0; i < newTiles.Length; i++)
        {

            newTiles[i] = GetRandomTile(percentBlank, percentBasic, percentIron, percentJelly, percentThird);
        }

        int rand = Random.Range(0, newTiles.Length);
        int stairLoc = rand;
        int spawnLoc = rand;

        while (stairLoc == spawnLoc)
        {
            spawnLoc = Random.Range(0, newTiles.Length);
        }

        newTiles[stairLoc] = new Tile(TileType.Stair, playerID);
        newTiles[spawnLoc] = new Tile(TileType.Spawn, playerID);

        return newTiles;

    }


}
