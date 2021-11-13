using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StakeoutState : FSM
{
    private float time;
    private float checkTime = 3;

    public override void Enter(NPC npc)
    {
        time = 0;
        npc.SetWalkState(false);
    }

    public override void Excute(NPC npc)
    {
        time += Time.deltaTime;
        if (time > checkTime) npc.ChangeState(npc.Criminal());        
    }

    public override void Exit(NPC npc)
    {
        npc.transform.position = npc.player.transform.position + new Vector3(-10.5f, 0);
    }
}
