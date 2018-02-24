// Date   : 24.02.2018 11:03
// Project: snipergame
// Author : bradur

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
        currentActiveCamera.enabled = false;

        if (cam == null)
        {
            currentActiveCamera = GameManager.main.MainCamera;
        }
        else
        {
            currentActiveCamera = cam.Camera;
        }

        currentActiveCamera.enabled = true;
    }
}