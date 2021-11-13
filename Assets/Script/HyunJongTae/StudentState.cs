using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentState : FSM
{
    private float time;
    private float checkTime = 3;
    private bool change;
    private bool stop;

    private bool end;

    public override void Enter(NPC npc)
    {
        change = false;
        stop = false;
        Debug.Log("Start Student State ");
        npc.SetWalkState(true);            
    }

    public override void Excute(NPC npc)
    {        
        npc.transform.Translate(Vector2.right * Time.deltaTime * npc.GetWalkSpeed());
        if (end) return;
        if (npc.reach)
        {
            end = true;
            SoundManager.Instance.StopBGM();
        }
        if (npc.player.Dir != Define.MoveDir.None)
        {
            stop = false;
            change = true;
        }
        else
        {
            if (!stop && change) stop = true;
        }
        if (stop)
        {
            if (RandomStop(40))
            {
                npc.ChangeState(npc.Idle());
            }
            else
            {
                stop = false;
                change = false;
                Debug.Log("Failed");
            }
        }             
    }

    public override void Exit(NPC npc)
    {
        Debug.Log("End Student State");       
    }

    private bool RandomStop(int percent)
    {
        return percent > Random.Range(0, 100) ? true : false;
    }
}
