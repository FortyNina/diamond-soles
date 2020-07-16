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

    [SerializeField]
    private GameObject[] _otherPlayers;


    private Tile[] _tiles;

    private float _adjustedX;
    private float _adjustedY;

    private int _currentFloor;


    void Awake()
    {
        _adjustedX = (-1 * ((_gridWidth * _tileWidth) / 2f) + _tileWidth / 2) + transform.position.x;
        _adjustedY = (-1 * ((_gridHeight * _tileWidth) / 2f) + _tileWidth / 2) + transform.position.y;

        //TODO: move to a select screen?
        GameData.Instance.playerMineLocations[playerID] = mineType;
        DisplayNewLayout();



    }

    private void Update()
    {
        //Check to Display other Players
        for(int i = 0;i < _otherPlayers.Length; i++)
        {
            bool playerOnFloor = false ;

            if (i == playerID)
                continue;

            if(GameData.Instance.playerMineLocations[i] == mineType)
            {
                if(mineType == Mine.IronMine)
                {
                    if(GameData.Instance.ironFloors[i] == GameData.Instance.ironFloors[playerID])
                    {
                        playerOnFloor = true;
                    }
                }
                if (mineType == Mine.JellyMine)
                {
                    if (GameData.Instance.jellyFloors[i] == GameData.Instance.jellyFloors[playerID])
                    {
                        playerOnFloor = true;
                    }
                }
                if (mineType == Mine.ThirdMine)
                {
                    if (GameData.Instance.thirdFloors[i] == GameData.Instance.thirdFloors[playerID])
                    {
                        playerOnFloor = true;
                    }
                }
            }

            if (playerOnFloor)
            {
                _otherPlayers[i].SetActive(true);
                _otherPlayers[i].transform.localPosition = GameData.Instance.playerLocalLocations[i];
            }
            else
                _otherPlayers[i].SetActive(false);


        }



        //Check for updated floors
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

        if (mineType == Mine.IronMine)
            _currentFloor = GameData.Instance.ironFloors[playerID];
        if (mineType == Mine.JellyMine)
            _currentFloor = GameData.Instance.jellyFloors[playerID];
        if (mineType == Mine.ThirdMine)
            _currentFloor = GameData.Instance.thirdFloors[playerID];

        if (MineRecorder.CheckMineFloorExists(mineType, _currentFloor))
        {
            _tiles = MineRecorder.GetMineFloor(mineType, _currentFloor);
        }
        else
        {
            _tiles = MineRecorder.CreateMineFloor(mineType,_currentFloor, _gridWidth, _gridHeight);
        }
        
        DrawGrid();
        StartCoroutine(RescanMap());

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
                else if (currentTile.tileType == TileType.Food)
                    prefabToGenerate = _jellyTile;
                else if (currentTile.tileType == TileType.Hole)
                    prefabToGenerate = _holeTile;

                if (prefabToGenerate != _blankTile)
                {

                    GameObject go = Instantiate(prefabToGenerate, new Vector3(0, 0, 0), Quaternion.identity);
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    Rock r = go.GetComponent<Rock>();

                    if (r != null)
                    {
                        r.CreateSettings(currentTile.oreAmount, currentTile.health, _currentFloor, mineType, i + j, currentTile);
                    }


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

    private IEnumerator RescanMap()
    {
        yield return new WaitForEndOfFrame();
        AStarMapController.RequestScan();

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
