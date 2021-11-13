using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator ani;
    public PlayerMove player { get; private set; }

    [SerializeField] private float walkSpeed;

    #region FSM
    private FSM idleState = new IdleState();
    private FSM studentState = new StudentState();
    private FSM criminalStaete = new CriminalState();
    private FSM stakeOutState = new StakeoutState();

    public FSM Idle() { return idleState; }
    public FSM Student() { return studentState; }
    public FSM Criminal() { return criminalStaete; }
    public FSM StakeOut() { return stakeOutState; }

    private FSM curState;

    #endregion regoin

    private void Awake()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    private void Start()
    {
        curState = StakeOut();
        curState.Enter(this);
    }

    private void FixedUpdate()
    {
        curState.Excute(this);

    }

    public void Event(int value)
    {
        if (value == 0)
        {
            curState = StakeOut();            
        }
        else if (value == 1)
        {
            curState = Student();            
        }
        curState.Enter(this);
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
