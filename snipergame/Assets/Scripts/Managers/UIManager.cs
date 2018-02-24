// Date   : 24.02.2018 08:49
// Project: snipergame
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

}
