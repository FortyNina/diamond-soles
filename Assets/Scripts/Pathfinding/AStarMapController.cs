using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarMapController : MonoBehaviour
{

    public AstarPath path;

    private static int _scanRequests = 0;

    // Update is called once per frame
    void Update()
    {
       
        if(_scanRequests > 0)
        {
            if (!path.isScanning)
            {
                path.Scan();
                _scanRequests--;
            }
        }

        if(_scanRequests < 0)
        {
            _scanRequests = 0;
        }

    }

    public static void RequestScan()
    {
        _scanRequests++;
    }

    

}
