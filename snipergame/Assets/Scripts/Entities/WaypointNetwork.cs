// Date   : 23.02.2018 22:54
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointNetwork : MonoBehaviour
{
    [SerializeField]
    private List<WaypointMarker> markers;
    public List<WaypointMarker> Markers { get { return markers; } }

    private List<WaypointMarker> drawn;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        List<WaypointMarker> deleted = new List<WaypointMarker>();
        if (drawn == null)
        {
            drawn = new List<WaypointMarker>();
        }

        markers = new List<WaypointMarker>();
        foreach (Transform child in transform)
        {
            if (child.tag == "WaypointMarker")
            {
                markers.Add(child.gameObject.GetComponent<WaypointMarker>());
            }
        }

        foreach (WaypointMarker marker in markers)
        {
            List<WaypointMarker> ns = marker.GetNeighbours();

            foreach (WaypointMarker n in ns)
            {
                if (n == null || n.Equals(null))
                {
                    deleted.Add(n);
                    continue;
                }

                if (!n.GetNeighbours().Contains(marker))
                {
                    n.AddNeighbour(marker);
                }

                if (!drawn.Contains(n))
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(marker.transform.position, n.transform.position);
                }
            }
        }
    }
}
