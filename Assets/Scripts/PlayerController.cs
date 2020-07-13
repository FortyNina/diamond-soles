using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public int playerID;

    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    protected GridCreator gridCreator;

    [SerializeField]
    public GameObject _axe;

    enum PlayerDir { left, right, up, down }

    private PlayerDir _direction;
    private bool _axeDown;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;



    // Start is called before the first frame update
    void Start()
    {
        playerID = gridCreator.playerID;
    }

    // Update is called once per frame
    void Update()
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
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            _axeDown = false;
            _axe.SetActive(false);

        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            GameData.Instance.ironFloors[playerID] = 10;
            gridCreator.DisplayNewLayout();
        }



        transform.position += new Vector3(x, y, 0);
        GameData.Instance.playerLocalLocations[playerID] = transform.localPosition;



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Staircase")
        {
            if(gridCreator.mineType == Mine.IronMine)
                GameData.Instance.ironFloors[playerID]++;
            if (gridCreator.mineType == Mine.JellyMine)
                GameData.Instance.jellyFloors[playerID]++;
            if (gridCreator.mineType == Mine.ThirdMine)
                GameData.Instance.thirdFloors[playerID]++;


            gridCreator.DisplayNewLayout();
            GameData.Instance.energyLevels[playerID] -= 1;

        }

        else if(collision.tag == "Hole")
        {
            int currentFloor = 0;
            if (gridCreator.mineType == Mine.IronMine)
                currentFloor = GameData.Instance.ironFloors[playerID];
            if (gridCreator.mineType == Mine.JellyMine)
                currentFloor = GameData.Instance.jellyFloors[playerID];
            if (gridCreator.mineType == Mine.ThirdMine)
                currentFloor = GameData.Instance.thirdFloors[playerID];

            int randFloors = 0;
            if (currentFloor < 15)
                randFloors = Random.Range(2, 4);
            else if (currentFloor < 35)
                randFloors = Random.Range(3, 9);
            else
                randFloors = Random.Range(5, 15);

            if (gridCreator.mineType == Mine.IronMine)
                GameData.Instance.ironFloors[playerID]+=randFloors;
            if (gridCreator.mineType == Mine.JellyMine)
                GameData.Instance.jellyFloors[playerID]+= randFloors;
            if (gridCreator.mineType == Mine.ThirdMine)
                GameData.Instance.thirdFloors[playerID]+= randFloors;
            gridCreator.DisplayNewLayout();
            GameData.Instance.energyLevels[playerID] -= randFloors;



        }

    }


   

}
