// Date   : 23.02.2018 22:48
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointMarker : MonoBehaviour
{
    [SerializeField]
    private List<WaypointMarker> neighbours;
    [SerializeField]
    private WaypointNetwork network;
    [SerializeField]
    private float waitTime;

    public WaypointNetwork Network { get { return network; } set { network = value;  } }
    public float WaitTime { get { return waitTime; } }

    void Start()
    {

    }

    void Update()
    {

    }

    public List<WaypointMarker> GetNeighbours()
    {
        return neighbours;
    }

    public void AddNeighbour(WaypointMarker marker)
    {
        neighbours.Add(marker);
    }

}