// Date   : 10.02.2018 19:26
// Project: 1HGJ-practice
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

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
    private SecurityCameraManager securityCameraManagerPrefab;

    [SerializeField]
    private TrailManager trailManagerPrefab;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private ShootGun shootGun;

    [SerializeField]
    private Camera mainCamera;
    public Camera MainCamera { get { return mainCamera; } }

    [Header("Settings")]
    // enable print debugs
    [SerializeField]
    private bool debugMode = false;
    public bool DebugMode { get { return debugMode; } }

    private string debugPrefix = "<b>[<color=green>GameManager</color>]:</b>";

    private LevelManager levelManager;

    void Start()
    {
        KeyManager keyManager = Instantiate(keyManagerPrefab);
        keyManager.transform.SetParent(transform, false);

        SoundManager soundManager = Instantiate(soundManagerPrefab);
        soundManager.transform.SetParent(transform, false);

        UIManager uiManager = Instantiate(uiManagerPrefab);
        uiManager.transform.SetParent(transform, false);

        TrailManager trailManager = Instantiate(trailManagerPrefab);
        trailManager.transform.SetParent(transform, false);

        levelManager = Instantiate(levelManagerPrefab);
        levelManager.transform.SetParent(transform, false);

        SecurityCameraManager securityCameraManager = Instantiate(securityCameraManagerPrefab);
        securityCameraManager.transform.SetParent(transform, false);

        levelManager.LoadNextLevel();
    }

    void Update()
    {

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
        shootGun.UpdateOrigin();
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

    public SecurityCamera GetSecurityCamera(int cam)
    {
        return levelManager.GetSecurityCamera(cam);
    }
}