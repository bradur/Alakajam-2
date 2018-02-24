// Date   : 24.02.2018 13:35
// Project: snipergame
// Author : M2tias

using UnityEngine;
using System.Collections;

public class FootPrint : MonoBehaviour
{
    //private float lifeTime;
    private float startLife;
    [SerializeField]
    private float maxLife;
    [SerializeField]
    private bool mirrored = false;
    [SerializeField]
    private MeshRenderer renderer;

    public bool Alive { get; set; }

    void Start()
    {
        Alive = true;
        startLife = Time.time;
    }

    void Update()
    {
        if (Alive)
        {
            float fadeout_coef = (maxLife - (Time.time - startLife)) / maxLife;
            if (Time.time - startLife > maxLife)
            {
                Alive = false;
            }

            if (mirrored)
            {
                renderer.materials[0].SetTextureScale("_MainTex", new Vector2(-1, 1));
            }
            else
            {
                renderer.materials[0].SetTextureScale("_MainTex", new Vector2(1, 1));
            }
            Color c = renderer.materials[0].color;
            Color nc = new Color(c.r, c.g, c.b, fadeout_coef);
            renderer.materials[0].color = nc;
            Debug.Log(fadeout_coef);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        Alive = true;
        startLife = Time.time;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);

    }
}
