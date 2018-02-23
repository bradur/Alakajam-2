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
    private LayerMask collidingLayers;

    private string debugPrefix = "<b>[Gun]:</b>";

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
        foreach (RaycastHit hitInfo in Physics.RaycastAll(shootOrigin, aim.forward * maxRayLength, maxRayLength, collidingLayers))
        {
            if (GameManager.main.DebugMode)
            {
                Debug.Log(string.Format(
                    "{0} Object {1} was hit at {2}.",
                    debugPrefix,
                    hitInfo.collider,
                    hitInfo.point
                ));
            }
        }
    }
}
