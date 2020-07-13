using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPathfinder : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (path == null)
        {
            print("5");

            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
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
        }
        else if(xDiff > .2f)
        {
            transform.position += new Vector3(-Time.deltaTime * 2f, 0, 0);

        }

        else if (yDiff < -.2f)
        {
            transform.position += new Vector3(0, Time.deltaTime * 2f, 0);

        }
        else if (yDiff > .2f)
        {
            transform.position += new Vector3(0, -Time.deltaTime * 2f,  0);


        }

        if(xDiff > -.2f && xDiff < .2f && yDiff > -.2f && yDiff< .2f)
        {
            currentWaypoint++;
        }



    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
