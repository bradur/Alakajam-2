// Date   : 10.02.2018 19:26
// Project: 1HGJ-practice
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager main;

    void Awake()
    {
        main = this;
    }

    [SerializeField]
    [Range(1, 20)]
    private int numberOfBullets = 5;

    // enable print debugs
    [SerializeField]
    private bool debugMode = false;
    public bool DebugMode { get { return debugMode; } }

    void Start () {
    
    }

    void Update () {
    
    }

    public bool SpendBullet()
    {
        if (numberOfBullets > 0)
        {
            numberOfBullets -= 1;
            return true;
        }
        return false;
    }

}
