// Date   : 23.02.2018 22:48
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

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

    private void OnDrawGizmos()
    {
        //Handles.Label(transform.position-new Vector3(0.2f, -0.7f, 0.2f), EditorGUIUtility.IconContent("lightMeter/greenLight").image);
    }
}