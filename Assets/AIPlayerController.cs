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

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    AIstate state = AIstate.TravelPath;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        DetermineNewTarget();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == AIstate.TravelPath)
        {

            if (path == null)
            {
                print("5");

                return;
            }

            //REACHED GOAL!
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                state = AIstate.Wait;
                DetermineNewTarget();
                return;
            }
            else
            {
                reachedEndOfPath = false;
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
            print(_direction.ToString());
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
        TileType toSeek = AIManager.GetTileTypeToSeek(playerID);
        Collider2D[] interactableObjects = Physics2D.OverlapCircleAll(transform.position, 100f); //TODO: make sure this doesnt overlap with other maps

        float minDist = 100;

        for(int i = 0;i < interactableObjects.Length; i++)
        {
            print(interactableObjects[i].name);
            if (toSeek == TileType.Stair && interactableObjects[i].gameObject.tag == "Staircase")
            {
                print("1");
                target = interactableObjects[i].transform;
                break;
            }
            if (toSeek == TileType.Hole && interactableObjects[i].gameObject.tag == "Hole")
            {
                print("2");
                target = interactableObjects[i].transform;
                break;
            }




            if(interactableObjects[i].GetComponent<Rock>() != null)
            {
                if(interactableObjects[i].GetComponent<Rock>().ore == toSeek)
                {
                    float dist = Vector2.Distance(transform.position, interactableObjects[i].transform.position);
                    if(dist < minDist)
                    {
                        minDist = dist;
                        target = interactableObjects[i].transform;
                        print("3");

                    }

                }
            }


        }

        seeker.StartPath(rb.position, target.position, OnPathComplete);

    }

    IEnumerator BreakBlock()
    {
        yield return new WaitForSeconds(.1f);

        //face block
        float xDiff = transform.position.x - target.position.x;
        float yDiff = transform.position.y - target.position.y;

        if(xDiff < -.5f)
        {
            _direction = PlayerDir.right;
        }
        if(xDiff > .5f)
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

        AxeDown();

        yield return new WaitForSeconds(.5f);

        AxeUp();

    }
}
