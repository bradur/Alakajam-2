// Date   : 23.02.2018 23:09
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;
using UnityEditor;

public class WaypointManager : MonoBehaviour
{
    [SerializeField]
    private WaypointNetwork selectedNetwork;
    private WaypointMarker selectedMarker;

    void Start()
    {

    }

    void Update()
    {

    }

    //Create a menu on the unity editor and assign a hotkey for the menu item (ctrl+g)
    [MenuItem("Waypoint System/Create neighbour %g")]
    static void CreateNeighbour()
    {
        //Find currently selected game object
        var selected = Selection.activeGameObject;

        if(selected != null && selected.tag == "WaypointMarker")
        {
            WaypointMarker selectedMarker = selected.GetComponent<WaypointMarker>();
            WaypointNetwork selectedNetwork = selectedMarker.Network;
            WaypointMarker obj = Resources.Load<WaypointMarker>("WaypointMarker");

            WaypointMarker newMarker = Instantiate(obj, selectedNetwork.transform) as WaypointMarker;
            newMarker.transform.localPosition = selectedMarker.transform.localPosition + Vector3.back;
            newMarker.Network = selectedNetwork;
            newMarker.AddNeighbour(selectedMarker);
            selectedMarker.AddNeighbour(newMarker);

            //Set the new object as the selected object
            Selection.activeGameObject = newMarker.gameObject;
        }
    }

    [MenuItem("Waypoint System/Add markers as neighbours #%g")]
    static void CombineMarkers()
    {
        //Multiple objects selected. Unity docs recommended Selection.transforms with scene view
        var selected = Selection.transforms;
        bool properSelections = true;
        foreach(Transform t in selected)
        {
            if(t == null || t.tag != "WaypointMarker")
            {
                properSelections = false;
            }
        }

        if (properSelections && selected.Length >= 2)
        {
            Transform t1 = selected[0];
            Transform t2 = selected[1];

            WaypointMarker selectedMarker1 = t1.GetComponent<WaypointMarker>();
            WaypointMarker selectedMarker2 = t2.GetComponent<WaypointMarker>();
            if (selectedMarker1.Network == selectedMarker2.Network)
            {
                selectedMarker1.AddNeighbour(selectedMarker2);
                selectedMarker2.AddNeighbour(selectedMarker1);
            }
        }
    }
}
