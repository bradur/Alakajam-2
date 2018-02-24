// Date   : 24.02.2018 21:13
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BulletHolePool : MonoBehaviour
{
    [SerializeField]
    private BulletHole bulletHolePrefab;
    [SerializeField]
    private int poolSize = 100;
    [SerializeField]
    private bool spawnMore;
    [SerializeField]
    private Transform bulletHoleContainer;

    private List<BulletHole> backupHoles;
    private List<BulletHole> currentHoles;

    public List<BulletHole> CurrentHoles { get { return currentHoles; } }

    void Start()
    {
        currentHoles = new List<BulletHole>();
        backupHoles = new List<BulletHole>();

        for (int i = 0; i < poolSize; i++)
        {
            BulletHole hole = Spawn();
            hole.enabled = false;
            backupHoles.Add(hole);
        }
    }

    private BulletHole Spawn()
    {
        BulletHole hole = Instantiate(bulletHolePrefab);
        hole.transform.SetParent(bulletHoleContainer, true);
        return hole;
    }

    void Update()
    {

    }

    public BulletHole GetBulletHole()
    {
        return WakeUp();
    }

    private BulletHole WakeUp()
    {
        if (backupHoles.Count <= 2)
        {
            if (spawnMore)
            {
                backupHoles.Add(Spawn());
            }
            else
            {
                BulletHole hole = currentHoles[0];
                currentHoles.RemoveAt(0);
                hole.Deactivate();
                backupHoles.Add(hole);
            }
        }
        BulletHole newHole = null;
        if (backupHoles.Count > 0)
        {
            newHole = backupHoles[0];
            backupHoles.RemoveAt(0);
            newHole.gameObject.SetActive(true);
            newHole.Activate();
            currentHoles.Add(newHole);
        }

        return newHole;
    }

}
