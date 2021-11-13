using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStringBuilder : MonoBehaviour
{
    private static GameStringBuilder _instance = null;
    public static GameStringBuilder Instance { get { return _instance; } set { _instance = value; } }


    [TextArea] public string[] StageFirstStrings;
    [TextArea] public string[] EventStrings;
    [TextArea] public string[] StageLastStrings;

    private void Awake()
    {
        _instance = this;
    }

    public static string StageFirstString(int index = 0)
    {
        return GameStringBuilder.Instance.StageFirstStrings[index];
    }

    public static string EventString(int index = 0)
    {
        return GameStringBuilder.Instance.EventStrings[index];
    }

    public static string StageLastString(int index = 0)
    {
        return GameStringBuilder.Instance.StageLastStrings[index];
    }
}
