// Date   : 24.02.2018 13:24
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrailPool : MonoBehaviour
{
    [SerializeField]
    private FootPrint trailPrefab;
    [SerializeField]
    private int poolSize = 100;
    [SerializeField]
    private bool spawnMore;
    [SerializeField]
    private Transform trailContainer;

    private List<FootPrint> backupTrail;
    private List<FootPrint> currentTrail;

    public List<FootPrint> CurrentTrail { get { return currentTrail; } }

    private int printCount = 0;

    void Start()
    {
        currentTrail = new List<FootPrint>();
        backupTrail = new List<FootPrint>();

        for(int i = 0; i< poolSize; i++)
        {
            backupTrail.Add(Spawn());
        }
    }

    void Update()
    {

    }

    private FootPrint Spawn()
    {
        FootPrint print = Instantiate(trailPrefab);
        printCount += 1;
        print.transform.SetParent(trailContainer, true);
        return print;
    }

    public void Sleep(FootPrint print)
    {
        currentTrail.Remove(print);
        print.transform.SetParent(trailContainer, true);
        print.gameObject.SetActive(false);
        backupTrail.Add(print);
    }

    public FootPrint GetFootPrint()
    {
        return WakeUp();
    }

    private FootPrint WakeUp()
    {
        if(backupTrail.Count <= 2)
        {
            if(spawnMore)
            {
                backupTrail.Add(Spawn());
            }
        }
        FootPrint newPrint = null;
        if(backupTrail.Count > 0)
        {
            newPrint = backupTrail[0];
            backupTrail.RemoveAt(0);
            newPrint.gameObject.SetActive(true);
            newPrint.Activate();
            currentTrail.Add(newPrint);
        }

        return newPrint;
    }
}
