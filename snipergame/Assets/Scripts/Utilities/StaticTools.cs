// Date   : 23.02.2018 23:40
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

public class StaticTools : MonoBehaviour
{
    public static int ClampInt(int inclusiveStart, int inclusiveEnd, int value)
    {
        if (value > inclusiveEnd)
        {
            return inclusiveEnd;
        }
        else if (value < inclusiveStart)
        {
            return inclusiveStart;
        }
        return value;
    }
}
