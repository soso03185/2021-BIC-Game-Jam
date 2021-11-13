using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSM
{
    private float time;
    private float checkTime = 5;    


    public override void Enter(NPC npc)
    {
        time = 0;                
        
        Debug.Log("Start Idle State");
        npc.SetWalkState(false);
    }

    public override void Excute(NPC npc)
    {
        if (npc.player.Dir == Define.MoveDir.Right) npc.ChangeState(npc.Criminal());       
        time += Time.deltaTime;         
    }

    public override void Exit(NPC npc)
    {
        Debug.Log("End Idle State");
    }
}
