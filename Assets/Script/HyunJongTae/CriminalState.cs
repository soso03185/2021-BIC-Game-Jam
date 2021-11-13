using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalState : FSM
{
    public override void Enter(NPC npc)
    {
        Debug.Log("Start Criminal State");
        npc.SetWalkState(true);
    }

    public override void Excute(NPC npc)
    {
        npc.transform.Translate(Vector2.right * Time.deltaTime * npc.GetWalkSpeed());
        if (npc.player.Dir == Define.MoveDir.None) npc.ChangeState(npc.Idle());       
    }

    public override void Exit(NPC npc)
    {
        Debug.Log("End Criminal State");
    }
}
