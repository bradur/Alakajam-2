// Date   : 24.02.2018 09:03
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{

    [SerializeField]
    [Range(1, 100)]
    private int numberOfBullets = 10;
    public int NumberOfBullets { get { return numberOfBullets; } }

    [SerializeField]
    private Transform playerPosition;

    [SerializeField]
    private string title = "";

    [SerializeField]
    private int numberOfEnemies = 2;

    private string debugPrefix = "<b>[<color=purple>Level</color>]:</b>";

    private bool levelFinished = false;

    public void DontSeeThroughDeadEnemies()
    {
        foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
        {
            enemy.DontSeeThroughIfDead();
        }
    }

    public void SeeThroughDeadEnemies()
    {
        foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
        {
            enemy.SeeThroughIfDead();
        }
    }

    [SerializeField]
    private List<SecurityCamera> securityCameras = new List<SecurityCamera>();
    public List<SecurityCamera> SecurityCameras { get { return securityCameras; } }

    public void Load()
    {
        if (title == "")
        {
            title = gameObject.name;
        }
        UIManager.main.SetTargetCount(numberOfEnemies);
        UIManager.main.ShowLevelTitle(title);
        if (playerPosition != null)
        {
            GameManager.main.SetPlayerPosition(playerPosition);
        }
        else
        {
            if (GameManager.main.DebugMode)
            {
                Debug.Log(
                    string.Format(
                        "{0} <b><color=red>WARNING!</color></b> player position isn't set.",
                        debugPrefix
                    )
                );
            }
        }
        UIManager.main.ShowSecurityCameraHUD(securityCameras.Count);
        UIManager.main.SetBulletCount(numberOfBullets);
    }

    public void Unload()
    {
        gameObject.SetActive(false);
    }

    public bool SpendBullet()
    {
        if (numberOfBullets > 0)
        {
            numberOfBullets -= 1;
            UIManager.main.SetBulletCount(numberOfBullets);
            return true;
        }
        return false;
    }

    // returns number of enemies left
    public int GetKills(int number)
    {
        numberOfEnemies -= number;
        if (numberOfEnemies <= 0)
        {
            numberOfEnemies = 0;
            UIManager.main.SetTargetCount(numberOfEnemies);
            GameManager.main.ExpectingNextLevel();
            return numberOfEnemies;
        }
        UIManager.main.SetTargetCount(numberOfEnemies);
        return numberOfEnemies;
    }
}
