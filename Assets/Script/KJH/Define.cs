using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum CharacterState
    {
        Idle,
        Moving,
        Run,
        Dead,
    }

    public enum MoveDir
    {
        None,
        Left,
        Right,
    }
}