using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator ani;
    private Transform player;

    [SerializeField] private float walkSpeed;

    #region FSM
    private FSM idleState = new IdleState();
    private FSM studentState = new StudentState();
    private FSM criminalStaete = new CriminalState();

    public FSM Idle() { return idleState; }
    public FSM Student() { return studentState; }
    public FSM Criminal() { return criminalStaete; }

    private FSM curState;

    #endregion regoin

    private void Awake()
    {
        ani = GetComponent<Animator>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        curState = Idle();
        curState.Enter(this);
    }

    private void FixedUpdate()
    {
        curState.Excute(this);

    }

    #region Setter
    public void ChangeState(FSM newState)
    {        
        curState.Exit(this);        
        curState = newState;
        curState.Enter(this);        
    }

    public void SetWalkState(bool value)
    {
        ani.SetBool("Walk", value);
    }

    #endregion

    #region Getter
    public float GetWalkSpeed() { return walkSpeed; }

    #endregion
}
