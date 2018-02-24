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

    [Header("Managers")]
    [SerializeField]
    private KeyManager keyManagerPrefab;

    [SerializeField]
    private SoundManager soundManagerPrefab;

    [SerializeField]
    private UIManager uiManagerPrefab;

    [SerializeField]
    private LevelManager levelManagerPrefab;

    [SerializeField]
    private Transform playerTransform;

    [Header("Settings")]
    // enable print debugs
    [SerializeField]
    private bool debugMode = false;
    public bool DebugMode { get { return debugMode; } }

    private string debugPrefix = "<b>[<color=green>GameManager</color>]:</b>";

    private LevelManager levelManager;

    void Start () {
        KeyManager keyManager = Instantiate(keyManagerPrefab);
        keyManager.transform.SetParent(transform, false);

        SoundManager soundManager = Instantiate(soundManagerPrefab);
        soundManager.transform.SetParent(transform, false);

        UIManager uiManager = Instantiate(uiManagerPrefab);
        uiManager.transform.SetParent(transform, false);

        levelManager = Instantiate(levelManagerPrefab);
        levelManager.transform.SetParent(transform, false);

        levelManager.LoadNextLevel();
    }

    void Update () {
    
    }

    public void SetPlayerPosition(Vector3 position)
    {
        if (DebugMode)
        {
            Debug.Log(string.Format(
                    "{0} Player position set to {1}.",
                    debugPrefix,
                    position
                )
            );
        }
        playerTransform.position = position;
    }

    public bool SpendBullet()
    {
        if (levelManager.SpendBullet())
        {
            return true;
        }
        if (DebugMode)
        {
            Debug.Log(string.Format(
                    "{0} Out of bullets.",
                    debugPrefix
                )
            );
        }
        return false;
    }

}
