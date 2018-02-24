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
    private float startWaitingTime = 0f;
    private bool waiting = false;

    private WaypointMarker prevPrevMarker;
    private WaypointMarker currentMarker;

    //turning
    private Vector3 direction;
    private float angleBetween;
    private float angleSpeed;

    void Start()
    {
    }

    void Update()
    {
        if(currentMarker == null)
        {
            currentMarker = network.Markers[0];
        }

        if(waiting)
        {
            if ((Time.time - startWaitingTime) >= waitTime)
            {
                waiting = false;
                waitTime = currentMarker.WaitTime;
            }

            angleSpeed += (angleBetween / waitTime) * Time.deltaTime;
            Debug.Log(angleSpeed);
            Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, angleSpeed*20, 0);
            Quaternion newQ = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, newQ, Time.deltaTime*(2/waitTime));
        }
        else
        {
            Vector2 newPos = Vector2.MoveTowards(xz(transform.position), xz(currentMarker.transform.position), 0.1f);
            this.transform.position = xzToVec3(newPos, transform.position);

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
                direction = currentMarker.transform.position - transform.position;

                angleBetween = Mathf.Deg2Rad * Vector3.Angle(transform.position, currentMarker.transform.position);

                angleSpeed = 0;
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
}
