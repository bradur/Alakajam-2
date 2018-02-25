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
        Bullet b = GameManager.main.GetBullet();
        if (Vector3.Distance(b.transform.position, transform.position) < 0.2f)
        {
            Debug.Log("lelele");
            Debug.Log("lelele");
            Debug.Log("lelele");
            Debug.Log("lelele");
            Debug.Log("lelele");
            Debug.Log("lölölö");
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
