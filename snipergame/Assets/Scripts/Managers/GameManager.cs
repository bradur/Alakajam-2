// Date   : 10.02.2018 19:26
// Project: 1HGJ-practice
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    private BulletHoleManager bulletHoleManagerPrefab;


    private ShootGun shootGun;

    private BulletCam bulletCam;

    private Bullet bullet;

    private CameraZoom cameraZoom;

    private Camera mainCamera;
    public Camera MainCamera { get { return mainCamera; } }

    private SimpleSmoothMouseLook mouseLook;


    [Header("Settings")]
    // enable print debugs
    [SerializeField]
    private bool debugMode = false;
    public bool DebugMode { get { return debugMode; } }

    private string debugPrefix = "<b>[<color=green>GameManager</color>]:</b>";

    private LevelManager levelManager;

    private SecurityCameraManager securityCameraManager;

    [SerializeField]
    private GameObject playerCharacterPrefab;
    private Transform playerTransform;

    private Quaternion originalPlayerRotation;

    [SerializeField]
    private Material seeThroughMaterial;

    public Material GetSeeThroughMaterial()
    {
        return seeThroughMaterial;
    }

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

        BulletHoleManager bulletHoleManager = Instantiate(bulletHoleManagerPrefab);
        bulletHoleManager.transform.SetParent(transform, false);

        levelManager = Instantiate(levelManagerPrefab);
        levelManager.transform.SetParent(transform, false);

        securityCameraManager = Instantiate(securityCameraManagerPrefab);
        securityCameraManager.transform.SetParent(transform, false);

        levelManager.LoadNextLevel();
    }

    private void CreatePlayer ()
    {
        GameObject player = Instantiate(playerCharacterPrefab);
        player.transform.SetParent(transform, false);
        shootGun = player.GetComponentInChildren<ShootGun>();
        bulletCam = player.GetComponentInChildren<BulletCam>();
        cameraZoom = player.GetComponentInChildren<CameraZoom>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mouseLook = player.GetComponentInChildren<SimpleSmoothMouseLook>();
        bullet = player.GetComponentInChildren<Bullet>();
        playerTransform = player.transform;
    }

    private bool expectingRestart = true;
    void Update()
    {
        if (expectingRestart && KeyManager.main.GetKeyUp(KeyTriggeredAction.Restart)) {
            playerTransform.gameObject.SetActive(false);
            UIManager.main.HideMessage();
            SetScopeVisibility(false);
            Time.timeScale = 1f;
            CreatePlayer();
            levelManager.ReloadLevel();
        }
    }

    public int GetNumberOfBullets ()
    {
        return levelManager.GetNumberOfBullets();
    }

    //
    public void SetCameraControlState(bool enabled)
    {
        mouseLook.enabled = enabled;
        cameraZoom.enabled = enabled;
        shootGun.enabled = enabled;
    }

    public void SetSecurityCameraControlState(bool allowed)
    {
        securityCameraManager.SetCameraControlState(allowed);
    }

    public void SetGunVisibility(bool visibility)
    {
        cameraZoom.SetGunVisibility(visibility);
    }

    public void SetScopeVisibility(bool visibility)
    {
        cameraZoom.SetScopeVisibility(visibility);
    }

    public void StartBulletCam(Vector3 start, Vector3 target, Vector3 direction, int numTargets, List<Enemy> enemies)
    {
        bulletCam.StartCam(start, target, direction, numTargets, enemies);
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
        if (playerTransform == null)
        {
            CreatePlayer();
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

    public void LoadNextLevel()
    {
        levelManager.LoadNextLevel();
    }

    public void GetKills(int number)
    {
        levelManager.GetKills(number);

    }

    public Bullet GetBullet()
    {
        return bullet;
    }
}