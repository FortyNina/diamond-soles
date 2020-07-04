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
    private GridCreator gridCreator;

    [SerializeField]
    public GameObject _axe;

    enum PlayerDir { left, right, up, down }

    private PlayerDir _direction;
    private bool _axeDown;

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

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && !_axeDown)
        {
            x -= _speed;
            _direction = PlayerDir.left;
        }

        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && !_axeDown)
        {
            x += _speed;
            _direction = PlayerDir.right;

        }

        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && !_axeDown)
        {
            y += _speed;
            _direction = PlayerDir.up;

        }

        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && !_axeDown)
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
        }
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            _axeDown = false;
            _axe.SetActive(false);

        }



        transform.position += new Vector3(x, y, 0);




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Staircase")
        {
            GameData.Instance.ironFloors[playerID]++;
            gridCreator.CreateNewLayout();
            GameData.Instance.energyLevels[playerID] -= 1;

        }

        else if(collision.tag == "Hole")
        {
            int randFloors = 0;
            if (GameData.Instance.ironFloors[playerID] < 15)
                randFloors = Random.Range(2, 4);
            else if (GameData.Instance.ironFloors[playerID] < 35)
                randFloors = Random.Range(3, 9);
            else
                randFloors = Random.Range(5, 15);

            GameData.Instance.ironFloors[playerID] += randFloors;
            gridCreator.CreateNewLayout();
            GameData.Instance.energyLevels[playerID] -= randFloors;



        }

    }


   

}
