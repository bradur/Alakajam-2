// Date   : 24.02.2018 10:44
// Project: snipergame
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTitle : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    private Animator animator;

    public void Show(string text)
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        txtComponent.text = text;
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

}
