// Date   : 24.02.2018 21:16
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

public class BulletHole : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer renderer;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
