// Date   : 23.04.2017 11:09
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public enum KeyTriggeredAction
{
    None,
}

[System.Serializable]
public class GameKey : System.Object
{
    public KeyCode key;
    public KeyTriggeredAction action;
}

public class KeyManager : MonoBehaviour
{

    [SerializeField]
    private bool debug;

    public static KeyManager main;

    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private List<GameKey> gameKeys = new List<GameKey>();

    public bool GetKeyDown(KeyTriggeredAction action)
    {
        if (Input.GetKeyDown(GetKeyCode(action)))
        {
            if (debug)
            {
                Debug.Log(string.Format("Key {0} pressed down to perform {1}.", GetKeyString(action), action.ToString()));
            }
            return true;
        }
        return false;
    }

    public bool GetKeyUp(KeyTriggeredAction action)
    {
        if (Input.GetKeyUp(GetKeyCode(action)))
        {
            if (debug)
            {
                Debug.Log(string.Format("Key {0} let up to perform {1}.", GetKeyString(action), action.ToString()));
            }
            return true;
        }
        return false;
    }

    public bool GetKey(KeyTriggeredAction action)
    {
        if (Input.GetKey(GetKeyCode(action)))
        {
            if (debug)
            {
                Debug.Log(string.Format("Key {0} held to perform {1}.", GetKeyString(action), action.ToString()));
            }
            return true;
        }
        return false;
    }

    public KeyCode GetKeyCode(KeyTriggeredAction action)
    {
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                return gameKey.key;
            }
        }
        return KeyCode.None;
    }

    public string GetKeyString(KeyTriggeredAction action)
    {
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                string keyString = gameKey.key.ToString();
                if (gameKey.key == KeyCode.Return)
                {
                    keyString = "Enter";
                }
                else if (gameKey.key == KeyCode.RightControl)
                {
                    keyString = "Right Ctrl";
                }
                return keyString;
            }
        }
        return "";
    }
}
