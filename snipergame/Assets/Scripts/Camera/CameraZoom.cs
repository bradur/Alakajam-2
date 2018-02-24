// Date   : 23.02.2018 23:21
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(SimpleSmoothMouseLook))]
public class CameraZoom : MonoBehaviour
{

    private SimpleSmoothMouseLook simpleSmoothMouseLook;

    [SerializeField]
    [Range(40, 120)]
    private float defaultFov = 80;

    [SerializeField]
    [Range(40, 120)]
    private float zoomedInFov = 50;

    [SerializeField]
    [Range(0, 90)]
    private float minZoomedInFov = 30;

    [SerializeField]
    private Vector2 sensitivityChangeFromFov = Vector3.one;

    // when zoomed in, zoomedInFov is applied
    [SerializeField]
    private bool zoomedIn = false;

    // when zoomed in and scrolling, scopedZoom = 0 is zoomedInFov
    // when scopedZoom is scopedZoomSteps, fov is minZoomedInFov
    private int scopedZoom = 0;

    private int scopedZoomSteps = 5;

    private Camera targetCamera;
    private Vector2 originalSensitivity;

    [SerializeField]
    private DrawLine drawVerticalLine;
    [SerializeField]
    private DrawLine drawHorizontalLine;

    [SerializeField]
    private GameObject gunModel;

    void Start()
    {
        simpleSmoothMouseLook = GetComponent<SimpleSmoothMouseLook>();
        originalSensitivity = simpleSmoothMouseLook.Sensitivity;
        targetCamera = GetComponent<Camera>();
        targetCamera.fieldOfView = defaultFov;
        if (GameManager.main.DebugMode && sensitivityChangeFromFov.magnitude >= simpleSmoothMouseLook.Sensitivity.magnitude)
        {
            Debug.Log("<b>[<color=red>WARNING</color>]:</b> Sensitivity Change From Fov must be smaller than SimpleSmoothMouseLook.sensitivity!");
        }
        if (zoomedInFov <= minZoomedInFov)
        {
            if (GameManager.main.DebugMode)
            {
                Debug.Log("<b>[<color=red>WARNING</color>]:</b> Zoomed In Fov must be smaller than Min Zoomed In Fov!");
            }
        }
    }

    public void SetScopeVisibility (bool visibility)
    {
        zoomedIn = visibility;
        UIManager.main.SetScopeVisibility(zoomedIn);
        drawVerticalLine.SetVisibility(zoomedIn);
        drawHorizontalLine.SetVisibility(zoomedIn);
        SetGunVisibility(!zoomedIn);
        if (zoomedIn)
        {
            UIManager.main.HideLevelTitle();
        }
    }

    public void SetGunVisibility(bool visibility)
    {
        gunModel.SetActive(visibility);
    }

    void Update()
    {
        float fov = defaultFov;

        if (KeyManager.main != null)
        {
            if (KeyManager.main.GetKeyUp(KeyTriggeredAction.ToggleScope))
            {
                SetScopeVisibility(!zoomedIn);
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
            scopedZoom = StaticTools.ClampInt(0, scopedZoomSteps, scopedZoom);

            fov = zoomedInFov - ((zoomedInFov - minZoomedInFov) / scopedZoomSteps) * scopedZoom;
        }
        float maxFovDifference = defaultFov - minZoomedInFov;
        float currentDifference = defaultFov - fov;
        float percentageDifference = currentDifference / maxFovDifference;
        // Change mouse sensitivity according to zoom (fov).
        // When zoom is max, mouse sensitivity is original - sensitivityChangeFromFov.
        // When zoom is min, mouse sensitivity is original.
        simpleSmoothMouseLook.Sensitivity = originalSensitivity - (sensitivityChangeFromFov * percentageDifference);
        targetCamera.fieldOfView = fov;
    }

}
