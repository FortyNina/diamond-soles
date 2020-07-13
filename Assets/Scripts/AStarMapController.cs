using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarMapController : MonoBehaviour
{

    public AstarPath path;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            path.Scan();
        }
    }
}
