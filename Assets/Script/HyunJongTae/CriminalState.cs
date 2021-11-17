using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CriminalState : FSM
{
    private float wTime;
    private float wCheckTime = 8;

    public override void Enter(NPC npc)
    {
        Debug.Log("Start Criminal State");
        npc.SetWalkState(true);           
    }

    public override void Excute(NPC npc)
    {        
        npc.transform.Translate(Vector2.right * Time.deltaTime * npc.GetWalkSpeed());
        if (npc.reach) npc.Ending();
        if (npc.player.Dir == Define.MoveDir.None) npc.ChangeState(npc.Idle());
        wTime += Time.deltaTime;
        if (wTime > wCheckTime)
        {
            npc.SetWalkSpeed(2f);
            wTime = 0;
        }
    }

    public override void Exit(NPC npc)
    {
        Debug.Log("End Criminal State");        
    }
}
