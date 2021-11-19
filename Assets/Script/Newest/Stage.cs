using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public const int TotalEventsAmount = 10;
    [SerializeField] public static int StageCount = 0;
    public static bool[] eventtriggers = new bool[TotalEventsAmount];
    public static List<int> currentActivatedTriggers = new List<int>();
    
    private void Awake()
    {
        currentActivatedTriggers = new List<int>();
    }

    private void OnEnable()
    {
        Cursor.visible = false;   
    }
}
