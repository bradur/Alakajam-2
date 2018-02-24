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

    [SerializeField]
    private Transform playerPosition;

    [SerializeField]
    private string title = "";

    [SerializeField]
    private float numberOfEnemies = 2;

    private string debugPrefix = "<b>[<color=purple>Level</color>]:</b>";

    private bool levelFinished = false;

    [SerializeField]
    private List<SecurityCamera> securityCameras = new List<SecurityCamera>();
    public List<SecurityCamera> SecurityCameras { get { return securityCameras; } }

    public void Load()
    {
        if (title == "")
        {
            title = gameObject.name;
        }
        UIManager.main.ShowLevelTitle(title);
        if (playerPosition != null)
        {
            GameManager.main.SetPlayerPosition(playerPosition.position);
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

    public void GetAKill()
    {
        if(numberOfEnemies > 0)
        {
            numberOfEnemies -= 1;
            //UIManager.main.SetEnemyCount(numberOfEnemies);
        }
        else
        {
            //UIManager.main.LevelFinished();
            GameManager.main.LoadNextLevel();
        }
    }
}
