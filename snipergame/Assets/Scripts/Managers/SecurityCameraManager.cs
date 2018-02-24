// Date   : 24.02.2018 11:03
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;

public class SecurityCameraManager : MonoBehaviour
{
    private Camera currentActiveCamera;

    void Start()
    {
        currentActiveCamera = GameManager.main.MainCamera;
    }

    void Update()
    {
        SetCamera(null);
        if (KeyManager.main != null)
        {
            if (KeyManager.main.GetKey(KeyTriggeredAction.ShowCameraOne))
            {
                SetCamera(GameManager.main.GetSecurityCamera(1));
            }
            else if (KeyManager.main.GetKey(KeyTriggeredAction.ShowCameraTwo))
            {
                SetCamera(GameManager.main.GetSecurityCamera(2));
            }
            else if (KeyManager.main.GetKey(KeyTriggeredAction.ShowCameraThree))
            {
                SetCamera(GameManager.main.GetSecurityCamera(3));
            }
            else if (KeyManager.main.GetKey(KeyTriggeredAction.ShowCameraFour))
            {
                SetCamera(GameManager.main.GetSecurityCamera(4));
            }
            else
            {
                SetCamera(null);
            }
        }
    }

    private void SetCamera(SecurityCamera cam)
    {
        Camera nextCam;
        Camera prevCam = null;
        if (cam == null)
        {
            nextCam = GameManager.main.MainCamera;
        }
        else
        {
            nextCam = cam.Camera;
        }

        if(currentActiveCamera != nextCam)
        {
            prevCam = currentActiveCamera;
        }

        currentActiveCamera = nextCam;
        currentActiveCamera.enabled = true;
        currentActiveCamera.gameObject.SetActive(true);
        currentActiveCamera.transform.parent.gameObject.SetActive(true);
        if (prevCam != null)
        {
            prevCam.enabled = false;
            prevCam.gameObject.SetActive(false);
            prevCam.transform.parent.gameObject.SetActive(false);
        }
    }
}
