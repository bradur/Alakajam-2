// Date   : 24.02.2018 08:49
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private List<Level> levels;

    private Level currentLevel;

    private bool initialized = false;

    [SerializeField]
    private int currentLevelNumber = 0;

    public void Initialize()
    {
        initialized = true;
    }

    public bool SpendBullet()
    {
        if (currentLevel.SpendBullet())
        {
            return true;
        }
        return false;
    }

    public bool LoadNextLevel()
    {
        if (!initialized)
        {
            Initialize();
        }
        else
        {
            currentLevelNumber += 1;
        }
        if (levels.Count > currentLevelNumber)
        {
            LoadLevel(currentLevelNumber);
            return true;
        }
        return false;
    }

    private void LoadLevel(int levelNumber)
    {
        if (currentLevel != null)
        {
            currentLevel.Unload();
        }
        currentLevel = Instantiate(levels[levelNumber]);
        currentLevel.transform.SetParent(transform, false);
        currentLevel.Load();
    }
}
