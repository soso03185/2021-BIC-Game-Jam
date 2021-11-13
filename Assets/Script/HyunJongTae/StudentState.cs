using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentState : FSM
{
    private float time;
    private float checkTime = 8;

    public override void Enter(NPC npc)
    {
        time = 0;
        Debug.Log("Start Student State ");
        npc.SetWalkState(true);
        npc.transform.position = npc.player.transform.position + new Vector3(-10.5f, 0);
    }

    public override void Excute(NPC npc)
    {
        if (checkTime < time) npc.ChangeState(npc.Idle());        
        npc.transform.Translate(Vector2.right * Time.deltaTime * npc.GetWalkSpeed());
        time += Time.deltaTime;        
    }

    public override void Exit(NPC npc)
    {
        Debug.Log("End Student State");       
    }
}
