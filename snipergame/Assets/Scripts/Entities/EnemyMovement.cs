// Date   : 24.02.2018 00:49
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private WaypointNetwork network;
    [SerializeField]
    private WaypointMarker prevMarker;
    [SerializeField]
    private float waitTime = 10f;
    [SerializeField]
    private float footPrintDelay = 0.2f;
    private float lastFootPrint;

    private float startWaitingTime = 0f;
    private bool waiting = false;

    private WaypointMarker prevPrevMarker;
    private WaypointMarker currentMarker;

    //turning
    private Vector3 direction;
    private float angleBetween;
    private float angleSpeed;

    [Range(0.5f, 10f)]
    [SerializeField]
    private float moveSpeed = 1f;


    void Start()
    {
        lastFootPrint = Time.time;
    }

    void Update()
    {
        if(currentMarker == null)
        {
            currentMarker = network.Markers[0]; 
            waiting = true;
            startWaitingTime = Time.time - waitTime;
        }

        if(waiting) // arrived to a node, wait until waitTime has passed
        {
            if ((Time.time - startWaitingTime) >= waitTime)
            {
                waiting = false;
                waitTime = currentMarker.WaitTime;
            }

            // rotate towards the new node while waiting
            angleSpeed += (angleBetween / waitTime) * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, angleSpeed*20, 0);
            Quaternion newQ = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, newQ, Time.deltaTime*(2/waitTime));
        }
        else // normal movement
        {
            Vector2 newPos = Vector2.MoveTowards(
                xz(transform.position),
                xz(currentMarker.transform.position),
                moveSpeed * Time.deltaTime
            );
            this.transform.position = xzToVec3(newPos, transform.position);

            // close enough to the next node. pick a new node that we didn't visit yet.
            if (Vector2.Distance(xz(transform.position), xz(currentMarker.transform.position)) < 0.1f)
            {
                waiting = true;
                startWaitingTime = Time.time;
                if (prevPrevMarker == null)
                {
                    if (prevMarker == null)
                    {
                        prevMarker = currentMarker;
                    }
                    else
                    {
                        prevPrevMarker = prevMarker;
                        prevMarker = currentMarker;
                    }
                }
                else
                {
                    prevPrevMarker = prevMarker;
                    prevMarker = currentMarker;
                }
                currentMarker = currentMarker.GetNeighbours().Where(x => x != prevMarker && x != prevPrevMarker).First();
                direction = replaceY(currentMarker.transform.position, transform.position) - transform.position;

                angleBetween = Mathf.Deg2Rad * Vector3.Angle(transform.position, replaceY(currentMarker.transform.position, transform.position));

                angleSpeed = 0;
            }

            angleSpeed += (angleBetween / waitTime) * Time.deltaTime;


            Vector3 newDir = Vector3.RotateTowards(transform.up, direction, angleSpeed*20, 0);
            if (newDir.magnitude > 0.05f)
            {
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            
            if ((Time.time - lastFootPrint) >= footPrintDelay)
            {
                lastFootPrint = Time.time;
                TrailManager.main.SpawnFootPrint(transform.localPosition, transform.localRotation);
            }
        }
    }

    private Vector2 xz(Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }

    private Vector3 xzToVec3(Vector2 vec, Vector3 original)
    {
        return new Vector3(vec.x, original.y, vec.y);
    }

    private Vector3 replaceY(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.x, vec2.y, vec1.z);
    }
}
