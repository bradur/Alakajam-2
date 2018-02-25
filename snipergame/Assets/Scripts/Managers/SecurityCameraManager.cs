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


    private bool canControlCameras = true;
    public void SetCameraControlState (bool allowed)
    {
        canControlCameras = allowed;
        SetCamera(-1);
        UIManager.main.SetCameraControlVisibility(allowed);
    }

    void Update()
    {
        if (canControlCameras && KeyManager.main != null)
        {
            if (KeyManager.main.GetKeyDown(KeyTriggeredAction.ShowCameraOne))
            {
                SetCamera(1);
            }
            else if (KeyManager.main.GetKeyDown(KeyTriggeredAction.ShowCameraTwo))
            {
                SetCamera(2);
            }
            else if (KeyManager.main.GetKeyDown(KeyTriggeredAction.ShowCameraThree))
            {
                SetCamera(3);
            }
            else if (KeyManager.main.GetKeyDown(KeyTriggeredAction.ShowCameraFour))
            {
                SetCamera(4);
            }
            else
            {
                if (KeyManager.main.GetKeyUp(KeyTriggeredAction.ShowCameraOne))
                {
                    SetCamera(-1);
                }
                else if (KeyManager.main.GetKeyUp(KeyTriggeredAction.ShowCameraTwo))
                {
                    SetCamera(-1);
                }
                else if (KeyManager.main.GetKeyUp(KeyTriggeredAction.ShowCameraThree))
                {
                    SetCamera(-1);
                }
                else if (KeyManager.main.GetKeyUp(KeyTriggeredAction.ShowCameraFour))
                {
                    SetCamera(-1);
                }
            }
        }
    }

    private int currentSecurityCameraIndex = -1;

    private void SetCamera(int securityCameraIndex)
    {
        SecurityCamera securityCamera;
        if (currentSecurityCameraIndex != -1)
        {
            UIManager.main.SetCameraActiveState(currentSecurityCameraIndex, false);

            if (GameManager.main.GetSecurityCamera(currentSecurityCameraIndex) != null)
            {
                securityCamera = GameManager.main.GetSecurityCamera(currentSecurityCameraIndex);
                securityCamera.Camera.enabled = false;
            }
        }
        if (securityCameraIndex != -1)
        {
            currentSecurityCameraIndex = securityCameraIndex;
            GameManager.main.SetScopeVisibility(false);
            GameManager.main.SetCameraControlState(false);
            UIManager.main.SetCameraActiveState(securityCameraIndex, true);

            if (GameManager.main.GetSecurityCamera(securityCameraIndex) != null)
            {
                securityCamera = GameManager.main.GetSecurityCamera(securityCameraIndex);
                securityCamera.Camera.enabled = true;
            }
        }
        else
        {
            GameManager.main.SetCameraControlState(true);
        }
    }

}
