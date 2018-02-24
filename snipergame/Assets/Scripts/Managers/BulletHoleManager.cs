// Date   : 24.02.2018 21:10
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletHoleManager : MonoBehaviour
{
    public static BulletHoleManager main;
    [SerializeField]
    private BulletHolePool pool;

    void Awake()
    {
        main = this;
    }

    void Start () {

    }

    void Update()
    {
    }

    public void SpawnBulletHole(Vector3 pos, Quaternion rotation)
    {
        BulletHole hole = pool.GetBulletHole();

        if (hole != null)
        {
            hole.transform.position = pos;
            hole.transform.localRotation = rotation;
        }
    }

    public BulletHole SpawnBulletHole()
    {
        BulletHole hole = pool.GetBulletHole();
        return hole;
    }

    public void DeleteBulletHoles()
    {

    }
}
