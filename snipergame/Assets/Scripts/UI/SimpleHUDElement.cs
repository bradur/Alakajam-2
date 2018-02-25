// Date   : 24.02.2018 09:10
// Project: snipergame
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleHUDElement : MonoBehaviour
{

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    private Color originalColor;

    private string originalText;

    private void Start()
    {
        if (imgComponent != null)
        {
            originalColor = imgComponent.color;
        }
        if (txtComponent != null)
        {
            originalText = txtComponent.text;
        }
    }

    public void SetText(string text)
    {
        txtComponent.text = text;
        originalText = text;
    }

    public void SetActiveState(bool active)
    {
        if (originalColor == null)
        {
            originalColor = imgComponent.color;
        }
        if (active)
        {
            imgComponent.color = colorVariable;
            txtComponent.text = "*";
        }
        else
        {
            imgComponent.color = originalColor;
            txtComponent.text = originalText;
        }
    }

    private int oldCount;

    public void SetInt(int count)
    {
        // animate here from oldcount?
        txtComponent.text = count.ToString();
        originalText = txtComponent.text;
        oldCount = count;
    }

}
