using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    protected enum PlayerDir { left, right, up, down }

    [HideInInspector]
    public int playerID;
    public int PlayerID
    {
        get { return playerID; }
        set { playerID = value; }
    }

    protected float _speed = 1;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    protected GridCreator _gridCreator;
    public GridCreator GridCreator
    {
        set { _gridCreator = value; }
    }

    protected GameObject _axe;
    public GameObject Axe
    {
        set { _axe = value; }
    }

    protected bool _isAI;
    public bool IsAI
    {
        get { return _isAI; }
        set { _isAI = value; }
    }




    protected PlayerDir _direction;
    protected bool _axeDown;

    [SerializeField]
    private KeyCode left;

    [SerializeField]
    private KeyCode right;

    [SerializeField]
    private KeyCode up;

    [SerializeField]
    private KeyCode down;


    // Update is called once per frame
    private void Update()
    {
        float x = 0;
        float y = 0;

        if(Input.GetKey(left) && !_axeDown)
        {
            x -= _speed;
            _direction = PlayerDir.left;
        }

        else if(Input.GetKey(right) && !_axeDown)
        {
            x += _speed;
            _direction = PlayerDir.right;

        }

        else if (Input.GetKey(up) && !_axeDown)
        {
            y += _speed;
            _direction = PlayerDir.up;

        }

        else if (Input.GetKey(down) && !_axeDown)
        {
            y -= _speed;
            _direction = PlayerDir.down;

        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            AxeDown();

        }
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            AxeUp();

        }

        
        transform.position += new Vector3(x, y, 0);
        GameData.Instance.playerLocalLocations[playerID] = transform.localPosition;



    }

    protected void AxeDown()
    {
        _axeDown = true;
        if (_direction == PlayerDir.up)
            _axe.transform.position = new Vector3(transform.position.x, transform.position.y + .8f, 0);
        if (_direction == PlayerDir.down)
            _axe.transform.position = new Vector3(transform.position.x, transform.position.y - .8f, 0);
        if (_direction == PlayerDir.left)
            _axe.transform.position = new Vector3(transform.position.x - .8f, transform.position.y, 0);
        if (_direction == PlayerDir.right)
            _axe.transform.position = new Vector3(transform.position.x + .8f, transform.position.y, 0);
        _axe.SetActive(true);

        //durability!
        GameData.Instance.durabilityLevels[playerID]--;
    }

    protected void AxeUp()
    {
        _axeDown = false;
        _axe.SetActive(false);
    }

    protected void TraverseStaircase()
    {
        if (_gridCreator.mineType == Mine.IronMine)
            GameData.Instance.ironFloors[playerID]++;
        if (_gridCreator.mineType == Mine.JellyMine)
            GameData.Instance.jellyFloors[playerID]++;
        if (_gridCreator.mineType == Mine.ThirdMine)
            GameData.Instance.thirdFloors[playerID]++;


        _gridCreator.DisplayNewLayout();
        GameData.Instance.energyLevels[playerID] -= 1;
    }

    protected void TraverseHole()
    {
        int currentFloor = 0;
        if (_gridCreator.mineType == Mine.IronMine)
            currentFloor = GameData.Instance.ironFloors[playerID];
        if (_gridCreator.mineType == Mine.JellyMine)
            currentFloor = GameData.Instance.jellyFloors[playerID];
        if (_gridCreator.mineType == Mine.ThirdMine)
            currentFloor = GameData.Instance.thirdFloors[playerID];

        int randFloors = 0;
        if (currentFloor < 15)
            randFloors = Random.Range(2, 4);
        else if (currentFloor < 35)
            randFloors = Random.Range(3, 9);
        else
            randFloors = Random.Range(5, 15);

        if (_gridCreator.mineType == Mine.IronMine)
            GameData.Instance.ironFloors[playerID] += randFloors;
        if (_gridCreator.mineType == Mine.JellyMine)
            GameData.Instance.jellyFloors[playerID] += randFloors;
        if (_gridCreator.mineType == Mine.ThirdMine)
            GameData.Instance.thirdFloors[playerID] += randFloors;
        _gridCreator.DisplayNewLayout();
        GameData.Instance.energyLevels[playerID] -= randFloors;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Staircase")
        {
            TraverseStaircase();
        }

        else if(collision.tag == "Hole")
        {
            TraverseHole();

        }

    }

    public void SetControlKeys(KeyCode u, KeyCode d, KeyCode l, KeyCode r)
    {
        up = u;
        down = d;
        left = l;
        right = r;
    }


   

}
