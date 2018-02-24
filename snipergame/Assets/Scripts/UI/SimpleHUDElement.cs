// Date   : 24.02.2018 09:10
// Project: snipergame
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleHUDElement : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    public void SetText(string text)
    {
        txtComponent.text = text;
    }

    private int oldCount;

    public void SetInt(int count)
    {
        // animate here from oldcount?
        txtComponent.text = count.ToString();
        oldCount = count;
    }

}
