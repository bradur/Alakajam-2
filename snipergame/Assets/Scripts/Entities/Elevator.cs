// Date   : 25.02.2018 09:52
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{

    private bool moving = false;

    private float lerpStartTime;

    [SerializeField]
    [Range(0.2f, 5f)]
    private float duration = 1f;

    [SerializeField]
    [Range(1f, 10f)]
    private float secondsToStayStill = 1;

    private float stayStillTimer = 0f;

    private bool goingUp = true;

    [SerializeField]
    private Transform targetPosition;

    private Vector3 _targetPosition;

    private Vector3 originalPosition;

    private Vector3 animationTargetPosition;
    private Vector3 animationOriginalPosition;


    void Start()
    {
        originalPosition = transform.position;
        _targetPosition = targetPosition.position;
    }

    void Update()
    {

        if (moving)
        {
            float percentageComplete = (Time.time - lerpStartTime) / duration;
            transform.position = Vector3.Lerp(animationOriginalPosition, animationTargetPosition, percentageComplete);
            if (percentageComplete >= 1f)
            {
                moving = false;
            }
        }
        else
        {
            stayStillTimer += Time.deltaTime;
            if (stayStillTimer > secondsToStayStill)
            {
                moving = true;
                lerpStartTime = Time.time;
                if (goingUp)
                {
                    animationTargetPosition = _targetPosition;
                    animationOriginalPosition = originalPosition;
                }
                else
                {
                    animationTargetPosition = originalPosition;
                    animationOriginalPosition = _targetPosition;
                }
                goingUp = !goingUp;
                stayStillTimer = 0f;
            }
        }
    }

    /*public bool Call(Vector3 position)
    {
        if (!moving && Vector3.Distance(transform.position, position) > 0.4f)
        {
            lerpStartTime = Time.time;
            startPosition = transform.position;
            targetPosition = startPosition;
            targetPosition.y = position.y;
            moving = true;
            currentButton = elevatorButton;
            return true;
        }
        return false;
    }*/
}
