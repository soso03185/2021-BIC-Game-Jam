using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSM
{
    private float time;
    private float checkTime = 1;
    private bool change;

    public override void Enter(NPC npc)
    {
        time = 0;
        change = false;
        Debug.Log("Start Idle State");
        npc.SetWalkState(false);
    }

    public override void Excute(NPC npc)
    {
        if (npc.player.Dir == Define.MoveDir.Right) change = true;
        if (change) time += Time.deltaTime;
        if (time > checkTime) npc.ChangeState(npc.prevState);
    }

    public override void Exit(NPC npc)
    {
        Debug.Log("End Idle State");
    }
}
