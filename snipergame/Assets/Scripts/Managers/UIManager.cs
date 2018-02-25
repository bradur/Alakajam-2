// Date   : 24.02.2018 08:49
// Project: snipergame
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public static UIManager main;

    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image cursor;

    [SerializeField]
    private SimpleHUDElement hudBullets;

    public void SetBulletCount(int count)
    {
        hudBullets.SetInt(count);
    }

    [SerializeField]
    private SimpleHUDElement messageDisplay;

    public void ShowMessage(string message)
    {
        messageDisplay.gameObject.SetActive(true);
        messageDisplay.SetText(message);
    }

    public string GetMessageText()
    {
        return messageDisplay.GetText();
    }

    public void HideMessage()
    {
        messageDisplay.gameObject.SetActive(false);
    }

    [SerializeField]
    private SimpleHUDElement hudTargets;

    public void SetTargetCount(int count)
    {
        hudTargets.SetInt(count);
    }

    [SerializeField]
    private LevelTitle levelTitle;

    public void ShowLevelTitle(string title)
    {
        levelTitle.Show(title);
    }

    public void HideLevelTitle()
    {
        levelTitle.Hide();
    }

    [SerializeField]
    private HUDScope hudScope;

    public void SetScopeVisibility(bool visible)
    {
        hudScope.SetVisibility(visible);
        cursor.enabled = !visible;
    }

    [SerializeField]
    private List<SimpleHUDElement> cameraIndicators;

    public void ShowSecurityCameraHUD(int numberOfCameras)
    {

        for (int i = 0; i < cameraIndicators.Count; i += 1)
        {
            if (numberOfCameras < i + 1) {
                cameraIndicators[i].gameObject.SetActive(false);
            } else
            {
                cameraIndicators[i].gameObject.SetActive(true);
            }
        }
    }

    public void SetCameraActiveState (int cameraNumber, bool active)
    {
        Debug.Log(string.Format("Set camera {0}: {1}", cameraNumber, active));
        cameraIndicators[cameraNumber - 1].SetActiveState(active);
    }

    [SerializeField]
    private GameObject cameraControlContainer;
    public void SetCameraControlVisibility(bool visibility) {
        cameraControlContainer.SetActive(visibility);
    }
}
