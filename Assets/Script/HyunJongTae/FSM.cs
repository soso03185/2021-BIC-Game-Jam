using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    public abstract void Enter(NPC npc);
    public abstract void Excute(NPC npc);
    public abstract void Exit(NPC npc);   
}
