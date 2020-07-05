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


    [SerializeField]
    private GameObject _holeTile;


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

        DisplayNewLayout();


    }

    private void Update()
    {
        if (MineRecorder.IronDirty && !MineRecorder.CheckFlag(Mine.IronMine, playerID))
        {
            if(mineType != Mine.IronMine)
            {
                MineRecorder.SetBackFlag(Mine.IronMine, playerID);
            }
            else
            {
                Vector3 pos = _playerObject.transform.position;
                DisplayNewLayout();
                MineRecorder.SetBackFlag(Mine.IronMine, playerID);
                _playerObject.transform.position = pos;
            }
        }
        if (MineRecorder.JellyDirty && !MineRecorder.CheckFlag(Mine.JellyMine, playerID))
        {
            if (mineType != Mine.JellyMine)
            {
                MineRecorder.SetBackFlag(Mine.JellyMine, playerID);
            }
            else
            {
                Vector3 pos = _playerObject.transform.position;
                DisplayNewLayout();
                MineRecorder.SetBackFlag(Mine.JellyMine, playerID);
                _playerObject.transform.position = pos;
            }
        }
        if (MineRecorder.ThirdDirty && !MineRecorder.CheckFlag(Mine.ThirdMine, playerID))
        {
            if (mineType != Mine.ThirdMine)
            {
                MineRecorder.SetBackFlag(Mine.ThirdMine, playerID);
            }
            else
            {
                Vector3 pos = _playerObject.transform.position;
                DisplayNewLayout();
                MineRecorder.SetBackFlag(Mine.ThirdMine, playerID);
                _playerObject.transform.position = pos;
            }
        }
    }

    public void DisplayNewLayout()
    {
        DestroyCurrentTiles();
        if (MineRecorder.CheckMineFloorExists(mineType, GameData.Instance.ironFloors[playerID]))
        {
            _tiles = MineRecorder.GetMineFloor(mineType, GameData.Instance.ironFloors[playerID]);
        }
        else
        {
            _tiles = MineRecorder.CreateMineFloor(mineType, GameData.Instance.ironFloors[playerID], _gridWidth, _gridHeight);
        }
        
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


                if (currentTile.tileType == TileType.Spawn)
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

                if (currentTile.tileType == TileType.Rock)
                    prefabToGenerate = _rockTile;
                else if (currentTile.tileType == TileType.Stair)
                    prefabToGenerate = _stairTile;
                else if (currentTile.tileType == TileType.Iron)
                    prefabToGenerate = _ironTile;
                else if (currentTile.tileType == TileType.Jelly)
                    prefabToGenerate = _jellyTile;
                else if (currentTile.tileType == TileType.Hole)
                    prefabToGenerate = _holeTile;

                if (prefabToGenerate != _blankTile)
                {

                    GameObject go = Instantiate(prefabToGenerate, new Vector3(0, 0, 0), Quaternion.identity);
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    Rock r = go.GetComponent<Rock>();

                    if (r != null)
                        r.CreateSettings(currentTile.oreAmount, currentTile.health, GameData.Instance.ironFloors[playerID], mineType, i + j, currentTile);


                    go.transform.parent = transform;
                    go.transform.position = new Vector3(x, y, 0);


                    if (currentTile.tileType == TileType.Spawn)
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

}
