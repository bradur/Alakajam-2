// Date   : 24.02.2018 14:54
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrailManager : MonoBehaviour
{
    public static TrailManager main;
    [SerializeField]
    private TrailPool pool;

    void Awake()
    {
        main = this;
    }

    void Start()
    {

    }

    void Update()
    {
        List<FootPrint> dead = new List<FootPrint>();
        foreach(FootPrint print in pool.CurrentTrail)
        {
            if(!print.Alive)
            {
                dead.Add(print);
            }
        }

        foreach(FootPrint d in dead)
        {
            d.Deactivate();
            pool.Sleep(d);
        }
    }

    public void SpawnFootPrint(Vector3 pos, Quaternion rotation)
    {
        FootPrint print = pool.GetFootPrint();

        if(print != null)
        {
            print.transform.position = pos;
            print.transform.localRotation = rotation;
        }
    }
}
