using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum CharacterState
    {
        Idle,
        Moving,
        Skill,
        Jump,
        Dash,
        WallJump,
        Dead,
    }

    public enum MoveDir
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }
    public enum Layer
    {
        Block = 6,
        Floor = 7,
        Player = 10,
    }
}