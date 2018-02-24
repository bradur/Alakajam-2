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
    private Image imgComponent;

    [SerializeField]
    private SimpleHUDElement hudBullets;

    public void SetBulletCount(int count)
    {
        hudBullets.SetInt(count);
    }

}
