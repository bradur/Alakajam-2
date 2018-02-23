// Date   : 23.04.2017 11:09
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public enum KeyTriggeredAction
{
    None,
    ToggleScope,
    IncreaseScopedZoom,
    DecreaseScopedZoom
}

public enum MouseScrollWheel
{
    None,
    ScrollWheelUp,
    ScrollWheelDown
}

[System.Serializable]
public class GameKey : System.Object
{
    public KeyCode key;
    public MouseScrollWheel mouseScrollWheel;
    public KeyTriggeredAction action;
}

public class KeyManager : MonoBehaviour
{

    [SerializeField]
    private bool debug;

    [SerializeField]
    private bool totalDebug = false;

    public static KeyManager main;

    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private List<GameKey> gameKeys = new List<GameKey>();

    [SerializeField]
    private string debugPrefix = "<b>[<color=green>Input</color>]:</b>";

    private void Update()
    {
        if (totalDebug)
        {
            if (Input.anyKey)
            {
                Debug.Log(
                    string.Format(
                        "{0} Key {1} pressed.",
                        debugPrefix,
                        Input.inputString
                    )
                );
            }
        }
    }

    public bool GetKeyDown(KeyTriggeredAction action)
    {
        if (Input.GetKeyDown(GetKeyCode(action)))
        {
            if (debug)
            {
                Debug.Log(
                    string.Format(
                        "{0} Key {1} pressed down to perform {2}.",
                        debugPrefix,
                        GetKeyString(action),
                        action.ToString()
                    )
                );
            }
            return true;
        }
        return false;
    }

    public bool GetKeyUp(KeyTriggeredAction action)
    {
        MouseScrollWheel wheel = GetMouseWheelType(action);
        if (wheel != MouseScrollWheel.None)
        {
            if (wheel == MouseScrollWheel.ScrollWheelUp && Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                return true;
            }
            if (wheel == MouseScrollWheel.ScrollWheelDown && Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                return true;
            }
        }
        if (Input.GetKeyUp(GetKeyCode(action)))
        {
            if (debug)
            {
                Debug.Log(
                    string.Format(
                        "{0} Key {1} let up to perform {2}.",
                        debugPrefix,
                        GetKeyString(action),
                        action.ToString()
                    )
                );
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
                Debug.Log(
                    string.Format(
                        "{0} Key {1} held down to perform {2}.",
                        debugPrefix,
                        GetKeyString(action),
                        action.ToString()
                    )
                );
            }
            return true;
        }
        return false;
    }

    public MouseScrollWheel GetMouseWheelType(KeyTriggeredAction action)
    {
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                return gameKey.mouseScrollWheel;
            }
        }
        return MouseScrollWheel.None;
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
