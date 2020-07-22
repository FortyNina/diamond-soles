using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPlayerController : PlayerController
{
    private enum AIstate { TravelPath, Wait}

    public Transform target;


    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    [Space(5)]
    public GameObject currentObj;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    AIstate state = AIstate.TravelPath;

    private Vector3 _previousPos;
    private float _stuckTimer;
    private bool _breakingBlock;
    private bool _isStuck;
    private int _stuckNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        DetermineNewTarget();
        _previousPos = transform.position;
        _stuckTimer = 3f;
        


    }

    private void Update()
    {
        if (!_breakingBlock)
        {


            //check if stuck
            //might need to rescan map, and set new target pos
            if (IsStandingStill(_previousPos, transform.position))
                _stuckTimer -= Time.deltaTime;
            else
                _stuckTimer = 3f;
            if (_stuckTimer < 0)
            {
                if (_stuckNumber > 5)
                    StartCoroutine(BreakBlock());
                else
                {
                    Debug.Log("Player " + playerID + "is stuck");
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == AIstate.TravelPath)
        {
            if(target == null)
                Debug.Log(playerID + " is targeting null");

            //REACHED GOAL!
            if (path != null)
            {
                if (currentWaypoint >= path.vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    state = AIstate.Wait;
                    StartCoroutine(BreakBlock());
                    return;
                }
            }
            else
            {
                reachedEndOfPath = false;
            }

            if (path == null)
            {
                DetermineNewTarget();
                _stuckNumber = 0;
                return;
            }

            //target has disappeared
            if(target == null || !target.gameObject.activeInHierarchy)
            {
                _stuckNumber = 0;
                DetermineNewTarget();
            }

           
            

            float xDiff = transform.position.x - path.vectorPath[currentWaypoint].x;
            float yDiff = transform.position.y - path.vectorPath[currentWaypoint].y;


            if (xDiff < -.2f)
            {
                transform.position += new Vector3(Time.deltaTime * 2f, 0, 0);
                _direction = PlayerDir.right;
            }
            else if (xDiff > .2f)
            {
                transform.position += new Vector3(-Time.deltaTime * 2f, 0, 0);
                _direction = PlayerDir.left;


            }

            else if (yDiff < -.2f)
            {
                transform.position += new Vector3(0, Time.deltaTime * 2f, 0);
                _direction = PlayerDir.up;


            }
            else if (yDiff > .2f)
            {
                transform.position += new Vector3(0, -Time.deltaTime * 2f, 0);
                _direction = PlayerDir.down;
            }

            if (xDiff > -.2f && xDiff < .2f && yDiff > -.2f && yDiff < .2f)
            {
                currentWaypoint++;
            }
        }

        else if(state == AIstate.Wait)
        {
            //dont do anything
        }


        

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            state = AIstate.TravelPath;
        }
    }

    void DetermineNewTarget()
    {
        Collider2D[] interactableObjects = Physics2D.OverlapCircleAll(transform.position, 10); //TODO: make sure this doesnt overlap with other maps

        Transform elevator = null;
        if ((GameData.Instance.playerMineLocations[playerID] == Mine.IronMine && GameData.Instance.ironFloors[playerID] == 0)
            || (GameData.Instance.playerMineLocations[playerID] == Mine.JellyMine && GameData.Instance.jellyFloors[playerID] == 0)
            || (GameData.Instance.playerMineLocations[playerID] == Mine.IronMine && GameData.Instance.coalFloors[playerID] == 0))
        {
            List<Collider2D> elevators = new List<Collider2D>();
            for(int i = 0;i < interactableObjects.Length; i++)
            {
                if (interactableObjects[i].tag == "Elevator") elevators.Add(interactableObjects[i]);
            }
            elevator = AIManager.ChooseElevator(playerID, elevators);
        }
        if(elevator != null)
        {
            target = elevator;
        }
        else
        {
            TileType toSeek = AIManager.GetTileTypeToSeek(playerID, _isStuck);
            target = AIManager.GetTargetedTileTransformFromMap(interactableObjects, toSeek, playerID);
        }
        if (target != null)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
            currentObj = target.gameObject;
        }
    }

    IEnumerator BreakBlock()
    {
        _breakingBlock = true;
        yield return new WaitForSeconds(.1f);

        //face block
        if (target != null)
        {
            float xDiff = transform.position.x - target.position.x;
            float yDiff = transform.position.y - target.position.y;

            if (xDiff < -.5f)
            {
                _direction = PlayerDir.right;
            }
            if (xDiff > .5f)
            {
                _direction = PlayerDir.left;
            }
            if (yDiff < -.5f)
            {
                _direction = PlayerDir.up;
            }
            if (yDiff > .5f)
            {
                _direction = PlayerDir.down;
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
        state = AIstate.TravelPath;
        _breakingBlock = false;
        _stuckNumber = 0;


    }

    private bool IsStandingStill(Vector3 prev, Vector3 current)
    {
        if(Vector3.Distance(prev, current) < .05f)
        {
            return true;
        }
        return false;

    }
}
