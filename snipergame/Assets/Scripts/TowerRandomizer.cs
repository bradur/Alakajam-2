// Date   : 25.02.2018 13:00
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerRandomizer : MonoBehaviour {

    void Start () {
        List<Transform> transforms = new List<Transform>();
        foreach (Transform child in transform)
        {
            transforms.Add(child);
        }

        foreach (Transform t in transforms) {
            int i = Random.Range(0, 4);
            Vector3 tmpPos = transforms[i].position;
            Quaternion tmpRot = transforms[i].rotation;
            transforms[i].position = t.transform.position;
            transforms[i].rotation = t.transform.rotation;
            t.transform.position = tmpPos;
            t.transform.rotation = tmpRot;
        }

    
    }

    void Update () {
    
    }
}
