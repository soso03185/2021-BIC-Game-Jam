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

    }

    public override void Exit(NPC npc)
    {
        Debug.Log("End Criminal State");
    }
}
