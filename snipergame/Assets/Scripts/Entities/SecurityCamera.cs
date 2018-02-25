// Date   : 24.02.2018 09:17
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField]
    private Axes rotationAxis;
    [SerializeField]
    [Range(0, 90)]
    private float rotationSpan;
    [SerializeField]
    [Range(0, 1)]
    private float rotationSpeed;
    [SerializeField]
    private int direction = 1;

    [SerializeField]
    private Camera camera;
    public Camera Camera { get { return camera; } }

    private Vector3 originalRotation;
    private float t = 0.5f;

    void Start()
    {
        originalRotation = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        Vector3 components = transform.localRotation.eulerAngles;

        if (rotationAxis == Axes.X)
        {

            components.x += 1;
        }
        else if (rotationAxis == Axes.Y)
        {
            float oy = originalRotation.y;
            float angle = oy;

            t += rotationSpeed * Time.deltaTime;

            if (direction == 1)
            {
                angle = Mathf.LerpAngle(oy, oy + rotationSpan, Mathf.Sin(t));
            }
            else
            {
                angle = Mathf.LerpAngle(oy + rotationSpan, oy, Mathf.Sin(t));
            }

            components.y = angle;

            if(t >= Mathf.PI*0.5f)
            {
                direction *= -1;
                t = 0;
            }
        }
        else
        {

        }

        Quaternion newAngle = Quaternion.Euler(components);
        transform.localRotation = newAngle;
    }

}


enum Axes
{
    X,
    Y,
    Z
}