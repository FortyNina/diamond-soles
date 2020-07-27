using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

[RequireComponent(typeof(Seeker))]
public class AIMinerController : MonoBehaviour
{
    
    private enum AIState { TravelPath, Wait }
    private enum PlayerDir { Left, Right, Up, Down}

    //Identity variables
    public int playerID;
    public GridCreator gridCreator;
    public GameObject axe;

    //Inspector-visible pathfinding elements (exposed for debugging)
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject currentObject;

    #region Private Attributes
    //Movement Levels
    private float _speed = 200;
    private float _nextWaypointDistance = 3f;

    //Pathfinding variables
    private Path _path;
    private int _currentWaypoint = 0;
    private bool _hasReachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D _rb;

    //AI miner states
    private AIState _state = AIState.TravelPath;
    private PlayerDir _direction;
    private bool _landedOnNewFloor;
    private bool _canMove = true;
    private bool _axeDown = false;

    //Stuck-preventing variables
    private Vector3 _previousPos;
    private float _stuckTimer;
    private bool _breakingBlock;
    private bool _isStuck;
    private int _stuckNumber = 0;
    #endregion

    public UnityEvent OnEnterStairCase;
    public UnityEvent OnEnterHole;
    public UnityEvent OnEnterElevator;
    public UnityEvent OnAxeSwing;


    /// <summary>
    /// Set up for the AIMinerController. Stuff like getting
    /// components and setting initial target
    /// </summary>

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        DetermineNewTarget();
        _previousPos = transform.position;
        _stuckTimer = 3f;
        axe.GetComponent<Axe>().PlayerID = gridCreator.playerID;

    }

    /// <summary>
    /// Handle Time-dependent actions not related to RB
    /// Handle building elevator, or determing if stuck
    /// </summary>
    private void Update()
    {
        //Debug
        if (Input.GetKeyUp(KeyCode.R)) //TODO: remove input
        {
            SeekRock();
        }

        //Handle Elevator Building
        if (_landedOnNewFloor)
        {
            _landedOnNewFloor = false;
            if (AIManager.BuildElevator(playerID))
            {
                //TODO: event? or look at playerController
            }
        }

        //Is Stuck Check only occurs if player isnt breakinga  block
        if (!_breakingBlock)
        {
            Debug.Log("Player " + playerID + " is not breaking a block");
            if(IsStandingStill(_previousPos, transform.position))
            {
                _stuckTimer -= Time.deltaTime;
            }
            else
            {
                _stuckTimer = 3f;
            }

            if (_stuckTimer < 0)
            {
                if (_stuckNumber > 5)
                {
                    Debug.Log("Player " + playerID + " has been stuck a while");
                    SeekRock();
                    _stuckNumber = 0;
                }
                else
                {
                    Debug.Log("Player " + playerID + " is stuck");
                    _stuckTimer = 3f;
                    _isStuck = true;
                    AStarMapController.RequestScan();
                    DetermineNewTarget();
                    _isStuck = false;
                    _stuckNumber++;
                }
            }
        }

        _previousPos = transform.position;
        GameData.Instance.playerLocalLocations[playerID] = transform.localPosition;

    }

    /// <summary>
    /// Handle AI miner movement here (pathfinding)
    /// </summary>
    private void FixedUpdate()
    {
        if (!_canMove)
            return;

        #region TravelPath
        if (_state == AIState.TravelPath)
        {
            //REACHED GOAL
            if(_path != null)
            {
                if(_currentWaypoint >= _path.vectorPath.Count)
                {
                    Debug.Log("Player " + playerID + " reached a goal");
                    _hasReachedEndOfPath = true;
                    _state = AIState.Wait;
                    if(!_breakingBlock)StartCoroutine(BreakBlock());
                    return;
                }
                
            }
            else
            {
                _hasReachedEndOfPath = false;
            }

            if (_path == null)
            {
                Debug.Log("Player " + playerID + " has a null path");
                DetermineNewTarget();
                _stuckNumber = 0;
                return;
            }

            //Target has disappeared. Probably was destroyed by another player
            if(target == null || !target.gameObject.activeInHierarchy)
            {
                Debug.Log("Player " + playerID + " has a null target");
                _stuckNumber = 0;
                DetermineNewTarget();
            }

            //Determine AI direction and actually move the AI
            float xDiff = transform.position.x - _path.vectorPath[_currentWaypoint].x;
            float yDiff = transform.position.y - _path.vectorPath[_currentWaypoint].y;

            if(xDiff < -.2f)
            {
                transform.position += new Vector3(Time.deltaTime * 2f, 0, 0);
                _direction = PlayerDir.Right;
            }

            else if (xDiff > .2f)
            {
                transform.position += new Vector3(-Time.deltaTime * 2f, 0, 0);
                _direction = PlayerDir.Left;


            }

            else if (yDiff < -.2f)
            {
                transform.position += new Vector3(0, Time.deltaTime * 2f, 0);
                _direction = PlayerDir.Up;


            }
            else if (yDiff > .2f)
            {
                transform.position += new Vector3(0, -Time.deltaTime * 2f, 0);
                _direction = PlayerDir.Down;
            }

            if (xDiff > -.2f && xDiff < .2f && yDiff > -.2f && yDiff < .2f)
            {
                _currentWaypoint++;
            }

        }
        #endregion

        #region Wait
        if(_state == AIState.Wait)
        {
            //Dont do anything
        }

        #endregion

    }

    /// <summary>
    /// For interacting with tiles in the world
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Staircase")
        {
            TraverseStaircase();
            OnEnterStairCase.Invoke();
        }

        else if (collision.tag == "Hole")
        {
            TraverseHole();
            OnEnterHole.Invoke();

        }
        else if (collision.tag == "Elevator")
        {
            UseElevator(collision.GetComponent<ElevatorObj>());
            OnEnterElevator.Invoke();
        }

    }

    /// <summary>
    /// For interacting with rocks
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rock>() != null)
            target = collision.gameObject.transform;
        _state = AIState.Wait;
        StartCoroutine(BreakBlock());
    }

    public void RunOutOfEnergy()
    {
        _canMove = false;
        //_runOutOfEnergyDisplay.SetActive(true); //TODO!
    }

    /// <summary>
    /// Called when a path has finished GENERATING without errors
    /// NOT when a player has reached the end of the path
    /// </summary>
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
            //_state = AIState.TravelPath;
        }
    }

    public void SetAxeID()
    {
        axe.GetComponent<Axe>().PlayerID = playerID;
    }

    /// <summary>
    /// Returns whether or not a transform is standing still THIS FRAME
    /// </summary>
    private bool IsStandingStill(Vector3 prev, Vector3 current)
    {
        if (Vector3.Distance(prev, current) < .05f)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Currently a debug method. sets the AI's target to just a rock
    /// </summary>
    private void SeekRock()
    {
        Collider2D[] interactableObjects = Physics2D.OverlapCircleAll(transform.position, 10); //TODO: make sure this doesnt overlap with other maps
        target = AIManager.GetTargetedTileTransformFromMap(interactableObjects, TileType.Rock, playerID, transform);
        if (target != null)
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
            currentObject = target.gameObject;
        }
    }

    /// <summary>
    /// Find the next target to seek, and then generate a path for it
    /// Also determine if the player should use an elevator
    /// </summary>
    private void DetermineNewTarget()
    {
        Collider2D[] interactableObjects = Physics2D.OverlapCircleAll(transform.position, 10); //TODO: make sure this doesnt overlap with other maps

        Transform elevator = null;
        Mine m = GameData.Instance.playerMineLocations[playerID];
        if ((m == Mine.IronMine && GameData.Instance.playerFloors[playerID][Mine.IronMine] == 0)
            || (m == Mine.JellyMine && GameData.Instance.playerFloors[playerID][Mine.JellyMine] == 0)
            || (m == Mine.IronMine && GameData.Instance.playerFloors[playerID][Mine.CoalMine] == 0))
        {
            List<Collider2D> elevators = new List<Collider2D>();
            for (int i = 0; i < interactableObjects.Length; i++)
            {
                if (interactableObjects[i].tag == "Elevator") elevators.Add(interactableObjects[i]);
            }
            elevator = AIManager.ChooseElevator(playerID, elevators);
        }
        if (elevator != null)
        {
            target = elevator;
        }
        else
        {
            TileType toSeek = AIManager.GetTileTypeToSeek(playerID, _isStuck);
            target = AIManager.GetTargetedTileTransformFromMap(interactableObjects, toSeek, playerID, transform);
        }
        if (target != null)
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
            currentObject = target.gameObject;
        }
    }

    /// <summary>
    /// Swing an axe and break the block over a certain period of time
    /// </summary>
    private IEnumerator BreakBlock()
    {

        _breakingBlock = true;
        yield return new WaitForSeconds(.1f);

        //face the block
        if(target != null)
        {
            Debug.Log("Player " + playerID + " is about to break a block");

            float xDiff = transform.position.x - target.position.x;
            float yDiff = transform.position.y - target.position.y;

            if (xDiff < -.5f)
            {
                _direction = PlayerDir.Right;
            }
            if (xDiff > .5f)
            {
                _direction = PlayerDir.Left;
            }
            if (yDiff < -.5f)
            {
                _direction = PlayerDir.Up;
            }
            if (yDiff > .5f)
            {
                _direction = PlayerDir.Down;
            }
            int health = 0;
            if (target.gameObject.GetComponent<Rock>() != null)
            {
                health = target.gameObject.GetComponent<Rock>().health;
            }

            while (health > 0)
            {

                AxeDown();

                yield return new WaitForSeconds(.5f);

                AxeUp();

                yield return new WaitForSeconds(.5f);
                health--;
            }
        }
        DetermineNewTarget();
        _state = AIState.TravelPath;
        _breakingBlock = false;
        _stuckNumber = 0;
    
    }

    private void AxeDown()
    {
        if (GameData.Instance.durabilityLevels[playerID] <= 0)
            return;
        Debug.Log("Player " + playerID + " is about to put their axe down");
        Vector3 t = Vector3.zero;
        if (_direction == PlayerDir.Up)
            t = new Vector3(transform.position.x, transform.position.y + .8f, 0);
        if (_direction == PlayerDir.Down)
            t = new Vector3(transform.position.x, transform.position.y - .8f, 0);
        if (_direction == PlayerDir.Left)
            t = new Vector3(transform.position.x - .8f, transform.position.y, 0);
        if (_direction == PlayerDir.Right)
            t = new Vector3(transform.position.x + .8f, transform.position.y, 0);

        SwingAxe(t);
    }

    protected void AxeUp()
    {
        _axeDown = false;
        axe.SetActive(false);
    }

    private void SwingAxe(Vector3 t)
    {
        _axeDown = true;
        axe.transform.position = t;
        axe.SetActive(true);
        OnAxeSwing.Invoke();

    }


    private void TraverseStaircase()
    {
        GameData.Instance.playerFloors[playerID][gridCreator.mineType]++;
        gridCreator.DisplayNewLayout();
        GameData.Instance.energyLevels[playerID] -= 1;
        _landedOnNewFloor = true;
    }

    private void TraverseHole()
    {
        int currentFloor = GameData.Instance.playerFloors[playerID][gridCreator.mineType];

        int randFloors = 0;
        if (currentFloor < 15)
            randFloors = Random.Range(2, 4);
        else if (currentFloor < 35)
            randFloors = Random.Range(3, 9);
        else
            randFloors = Random.Range(5, 15);

        GameData.Instance.playerFloors[playerID][gridCreator.mineType] += randFloors;

        gridCreator.DisplayNewLayout();
        GameData.Instance.energyLevels[playerID] -= randFloors;
        _landedOnNewFloor = true;


    }

    protected void UseElevator(ElevatorObj eo)
    {
        GameData.Instance.playerFloors[playerID][gridCreator.mineType] = eo.floor;
        GameData.Instance.playerMoney[playerID] -= eo.price;
        GameData.Instance.playerMoney[eo.playerID] += eo.price;
        gridCreator.DisplayNewLayout();

    }

    






}
