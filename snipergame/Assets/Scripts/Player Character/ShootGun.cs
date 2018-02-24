// Date   : 24.02.2018 00:20
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

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
    [Range(10f, 100f)]
    private float maxRayLength = 50f;

    [SerializeField]
    private Transform aim;

    [SerializeField]
    private LayerMask primitiveEnemyLayer;

    [SerializeField]
    private LayerMask complexEnemyLayer;

    private string debugPrefix = "<b>[<color=cyan>Gun</color>]:</b>";

    void Start()
    {
        timeSinceLastShot = minShootInterval;
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
        }
        // if primitive colliders were hit, recast ray
        if (simpleHits.Length > 0)
        {
            RaycastHit[] complexHits = Physics.RaycastAll(shootOrigin, aim.forward * maxRayLength, maxRayLength, complexEnemyLayer);
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
                // handle hit logic here
            }
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
}
