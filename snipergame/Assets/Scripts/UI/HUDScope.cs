// Date   : 24.02.2018 12:33
// Project: snipergame
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScope : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    public void SetVisibility(bool visible)
    {
        imgComponent.gameObject.SetActive(visible);
    }

}
