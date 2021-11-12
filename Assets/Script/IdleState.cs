using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSM
{
    private float time;
    private float checkTime = 5;

    private Vector2 startPos;


    public override void Enter(NPC npc)
    {
        time = 0;                
        
        Debug.Log("Start Idle State" + " " + startPos);
        npc.SetWalkState(false);
    }

    public override void Excute(NPC npc)
    {
        if (checkTime < time) npc.ChangeState(npc.Student());
        time += Time.deltaTime;         
    }

    public override void Exit(NPC npc)
    {
        Debug.Log("End Idle State");
    }
}
