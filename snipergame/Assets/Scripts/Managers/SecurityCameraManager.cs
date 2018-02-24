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

    private SecurityCamera currentSecurityCamera = null;

    private void SetCamera(SecurityCamera securityCamera)
    {
        if (currentSecurityCamera != null)
        {
            currentSecurityCamera.Camera.enabled = false;
        }
        if (securityCamera != null)
        {
            GameManager.main.SetScopeVisibility(false);
            currentSecurityCamera = securityCamera;
            currentSecurityCamera.Camera.enabled = true;
            GameManager.main.SetCameraControlState(false);
        }
        else
        {
            GameManager.main.SetCameraControlState(true);
        }
    }

}
