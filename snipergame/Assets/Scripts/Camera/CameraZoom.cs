// Date   : 23.02.2018 23:21
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraZoom : MonoBehaviour
{


    [SerializeField]
    [Range(40, 120)]
    private float defaultFov = 80;

    [SerializeField]
    [Range(40, 120)]
    private float zoomedInFov = 50;

    [SerializeField]
    [Range(0, 90)]
    private float minZoomedInFov = 30;

    // when zoomed in, zoomedInFov is applied
    [SerializeField]
    private bool zoomedIn = false;

    // when zoomed in and scrolling, scopedZoom = 0 is zoomedInFov
    // when scopedZoom is scopedZoomSteps, fov is minZoomedInFov
    private int scopedZoom = 0;

    private int scopedZoomSteps = 5;

    private Camera targetCamera;

    void Start()
    {
        targetCamera = GetComponent<Camera>();
        targetCamera.fieldOfView = defaultFov;
        if (zoomedInFov <= minZoomedInFov)
        {
            if (GameManager.main.DebugMode)
            {
                Debug.Log("<b>[<color=red>WARNING</color>]:</b>zoomedInFov must be smaller than minZoomedInFov!");
            }
        }
    }

    void Update()
    {
        float fov = defaultFov;

        if (KeyManager.main != null)
        {
            if (KeyManager.main.GetKeyUp(KeyTriggeredAction.ToggleScope))
            {
                zoomedIn = !zoomedIn;
            }
        }

        if (zoomedIn)
        {
            if (KeyManager.main.GetKeyUp(KeyTriggeredAction.IncreaseScopedZoom))
            {
                scopedZoom += 1;
            }
            else if (KeyManager.main.GetKeyUp(KeyTriggeredAction.DecreaseScopedZoom))
            {
                scopedZoom -= 1;
            }
            scopedZoom = Tools.ClampInt(0, scopedZoomSteps, scopedZoom);

            fov = zoomedInFov - ((zoomedInFov - minZoomedInFov) / scopedZoomSteps) * scopedZoom;
        }

        targetCamera.fieldOfView = fov;
    }

}
