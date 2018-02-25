// Date   : 24.02.2018 00:20
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootGun : MonoBehaviour
{

    [SerializeField]
    [Range(0, 1)]
    private float minShootInterval = 1f;

    private float timeSinceLastShot = 0f;

    [SerializeField]
    private Transform shootOriginTransform;

    private Vector3 shootOrigin;

    [SerializeField]
    [Range(10f, 1000f)]
    private float maxRayLength = 500f;

    [SerializeField]
    private Transform aim;

    [SerializeField]
    private LayerMask primitiveEnemyLayer;

    [SerializeField]
    private LayerMask complexEnemyLayer;

    [SerializeField]
    private LayerMask buildingLayer;

    private string debugPrefix = "<b>[<color=cyan>Gun</color>]:</b>";

    void Start()
    {
        timeSinceLastShot = minShootInterval;
        
    }

    public void UpdateOrigin ()
    {
        shootOrigin = shootOriginTransform.position;
    }

    void Update()
    {
        Debug.DrawRay(shootOrigin, aim.forward * maxRayLength, Color.red);
        timeSinceLastShot += Time.deltaTime;
        if (KeyManager.main.GetKeyUp(KeyTriggeredAction.ShootGun))
        {
            if ((timeSinceLastShot > minShootInterval) && GameManager.main.SpendBullet())
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
    }

    private void Shoot()
    {
        RaycastHit[] simpleHits = Physics.RaycastAll(shootOrigin, aim.forward * maxRayLength, maxRayLength, primitiveEnemyLayer, QueryTriggerInteraction.Collide);
        BulletHoleRays();
        Vector3 furthestPoint = Vector3.zero;
        foreach (RaycastHit hitInfo in simpleHits)
        {
            if (GameManager.main.DebugMode)
            {
                Debug.Log(string.Format(
                    "{0} Simple Enemy {1} was hit at {2}.",
                    debugPrefix,
                    hitInfo.collider,
                    hitInfo.point
                ));
            }
            Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GunRayCastHit();
            }
            if (furthestPoint.magnitude < hitInfo.point.magnitude)
            {
                furthestPoint = hitInfo.point;
            }
        }
        // if primitive colliders were hit, recast ray
        if (simpleHits.Length > 0)
        {
            RaycastHit[] complexHits = Physics.RaycastAll(shootOrigin, aim.forward * maxRayLength, maxRayLength, complexEnemyLayer);
            

            List<Enemy> enemiesHit = new List<Enemy>();

            foreach (RaycastHit hitInfo in complexHits)
            {
                if (GameManager.main.DebugMode)
                {
                    Debug.Log(string.Format(
                        "{0} <b><color=blue>COMPLEX</color></b> Enemy {1} was hit at {2}.",
                        debugPrefix,
                        hitInfo.collider,
                        hitInfo.point
                    ));
                }
                Enemy enemy = hitInfo.transform.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemiesHit.Add(enemy);
                }
                // handle hit logic here
                
            }
            float overShoot = 0.5f;
            if (complexHits.Length == 0)
            {
                overShoot = 10f;
            }
            furthestPoint = furthestPoint + aim.forward.normalized * overShoot;

            GameManager.main.SetScopeVisibility(false);
            GameManager.main.StartBulletCam(shootOrigin, furthestPoint, aim.forward, complexHits.Length, enemiesHit);

            foreach (RaycastHit hitInfo in simpleHits)
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.ResetCollider();
                }
            }
        }
    }

    private void BulletHoleRays()
    {
        int count = 0;
        bool goBack = false;
        RaycastHit[] wallHit = Physics.RaycastAll(shootOrigin, aim.forward * maxRayLength, maxRayLength, buildingLayer, QueryTriggerInteraction.Collide);
        RaycastHit lastHit = default(RaycastHit);
        while(wallHit.Length > 0)
        {
            RaycastHit firstHit = wallHit[0];
            Vector3 newOrigin = firstHit.point + aim.forward * 0.01f;
            Vector3 spawnPoint = firstHit.point - aim.forward * 0.01f;
            Vector3 normal = firstHit.normal; 
            BulletHole hole = BulletHoleManager.main.SpawnBulletHole();
            //hole.transform.LookAt(firstHit.point + firstHit.normal);
            hole.transform.localRotation = Quaternion.LookRotation(Vector3.up, firstHit.normal);
            hole.transform.position = spawnPoint;
            wallHit = Physics.RaycastAll(newOrigin, aim.forward * maxRayLength, maxRayLength, buildingLayer, QueryTriggerInteraction.Collide);
            if(wallHit.Length == 0)
            {
                lastHit = firstHit;
            }
            count++;
            goBack = true;
        }

        if(!goBack)
        {
            Debug.Log(count);
            return;
        }
        wallHit = Physics.RaycastAll(lastHit.point+aim.forward*0.1f, (-1*aim.forward) * maxRayLength, maxRayLength, buildingLayer, QueryTriggerInteraction.Collide);

        while(wallHit.Length > 0)
        {
            RaycastHit firstHit = wallHit[0];
            Vector3 newOrigin = firstHit.point - aim.forward * 0.01f;
            Vector3 spawnPoint = firstHit.point + aim.forward * 0.01f;
            Vector3 normal = firstHit.normal;
            BulletHole hole = BulletHoleManager.main.SpawnBulletHole();
            //hole.transform.LookAt(firstHit.point + firstHit.normal);
            hole.transform.localRotation = Quaternion.LookRotation(Vector3.up, firstHit.normal);
            hole.transform.position = spawnPoint;
            wallHit = Physics.RaycastAll(newOrigin, (-1 * aim.forward) * maxRayLength, maxRayLength, buildingLayer, QueryTriggerInteraction.Collide);

            count++;
        }

        Debug.Log(count);
    }
}
