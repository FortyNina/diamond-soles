using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mine { IronMine, JellyMine, CoalMine, Entry}

public class GridCreator : MonoBehaviour
{

   
    public int playerID;

    public Mine mineType = Mine.Entry;

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
    private GameObject _coalTile;

    [SerializeField]
    private GameObject _diamondTile;


    [SerializeField]
    private GameObject _holeTile;

    [SerializeField]
    private GameObject _elevatorTile;


    [Space(9)]
    [SerializeField]
    public GameObject _playerObject;

    [SerializeField]
    public GameObject[] _otherPlayers;


    private Tile[] _tiles;

    private float _adjustedX;
    private float _adjustedY;
    private float _elevatorStartX = -4.5f;
    private float _elevatorStartY = 3;

    private int _currentFloor;

    private TileType[] _prevTileTypes;
    private GameObject[] _tilesInScene;

    private bool _mineSelected = false;

    void Awake()
    {
        _adjustedX = (-1 * ((_gridWidth * _tileWidth) / 2f) + _tileWidth / 2) + transform.position.x;
        _adjustedY = (-1 * ((_gridHeight * _tileWidth) / 2f) + _tileWidth / 2) + transform.position.y;

    }

    private void Update()
    {
        if (!_mineSelected)
            return;

        //Check to Display other Players
        for(int i = 0;i < _otherPlayers.Length; i++)
        {
            bool playerOnFloor = false ;

            if (i == playerID)
                continue;

            if(GameData.Instance.playerMineLocations[i] == mineType)
            {
                if (GameData.Instance.playerFloors[i][mineType] == GameData.Instance.playerFloors[playerID][mineType])
                    playerOnFloor = true;
            }

            if (playerOnFloor)
            {
                _otherPlayers[i].SetActive(true);
                _otherPlayers[i].transform.localPosition = GameData.Instance.playerLocalLocations[i];
                Debug.Log(GameData.Instance.playerLocalLocations[i]);
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
                RemoveTilesFromLayout();
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
                RemoveTilesFromLayout();
                MineRecorder.SetBackFlag(Mine.JellyMine, playerID);
                _playerObject.transform.position = pos;
            }
        }
        if (MineRecorder.CoalDirty && !MineRecorder.CheckFlag(Mine.CoalMine, playerID))
        {
            if (mineType != Mine.CoalMine)
            {
                MineRecorder.SetBackFlag(Mine.CoalMine, playerID);
            }
            else
            {
                Vector3 pos = _playerObject.transform.position;
                RemoveTilesFromLayout();
                MineRecorder.SetBackFlag(Mine.CoalMine, playerID);
                _playerObject.transform.position = pos;
            }
        }
    }

    public void SelectMine(Mine mine)
    {
        mineType = mine;
        _mineSelected = true;
        GameData.Instance.playerMineLocations[playerID] = mineType;
        DisplayNewLayout();
    }

    public void RemoveTilesFromLayout()
    {
        for(int i = 0; i < _prevTileTypes.Length; i++)
        {
            if(_prevTileTypes[i] != MineRecorder.GetMineFloor(mineType, _currentFloor)[i].tileType)
            {
                _tilesInScene[i].SetActive(false) ;
            }
        }

        _prevTileTypes = MakeCopyOfTileSetTypes(MineRecorder.GetMineFloor(mineType, _currentFloor));
    }

   

    public void DisplayNewLayout()
    {
        DestroyCurrentTiles();
        _currentFloor = GameData.Instance.playerFloors[playerID][GameData.Instance.playerMineLocations[playerID]];
        
        if (MineRecorder.CheckMineFloorExists(mineType, _currentFloor))
        {
            _tiles = MineRecorder.GetMineFloor(mineType, _currentFloor);
            _prevTileTypes = MakeCopyOfTileSetTypes(_tiles);
        }
        else
        {
            _tiles = MineRecorder.CreateMineFloor(mineType,_currentFloor, _gridWidth, _gridHeight);
            _prevTileTypes = MakeCopyOfTileSetTypes(_tiles);

        }

        DrawGrid();
        StartCoroutine(RescanMap());

    }

    private void DrawGrid()
    {
        float x = _adjustedX;
        float y = _adjustedY;
        

        _tilesInScene = new GameObject[_gridHeight * _gridWidth];

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
                else if (currentTile.tileType == TileType.Coal)
                    prefabToGenerate = _coalTile;
                else if (currentTile.tileType == TileType.Diamond)
                    prefabToGenerate = _diamondTile;

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

                    _tilesInScene[i+j] = go;

                }


                x += _tileWidth;
            }
            x = _adjustedX;
            y += _tileWidth;
        }
        if (_currentFloor == 0)
            SetElevators();
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

    private TileType[] MakeCopyOfTileSetTypes(Tile[] original)
    {
        TileType[] newSet = new TileType[original.Length];
        for(int i = 0;i < original.Length; i++)
        {
            newSet[i] = original[i].tileType;
        }
        return newSet;

    }

    private void SetElevators()
    {
        float x = _elevatorStartX;
        for(int i =0;i < GameData.Instance.playerElevators.Count; i++)
        {
            for(int j = 0;j < GameData.Instance.playerElevators[i].Count; j++)
            {
                if(mineType == GameData.Instance.playerElevators[i][j].mineType)
                {
                    GameObject et = Instantiate(_elevatorTile, Vector3.zero, Quaternion.identity);
                    et.transform.parent = transform;
                    et.transform.localPosition = new Vector3(x, _elevatorStartY, 0);
                    et.GetComponent<ElevatorObj>().SetData(GameData.Instance.playerElevators[i][j]);
                    x += 3f;
;                        
                }
            }
        }
    }


}
