using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator ani;
    public PlayerMove player { get; private set; }
    private AudioSource myAudio;

    [SerializeField] private float walkSpeed;
    [SerializeField] private int eventValue;

    private bool stop = true;

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
    public FSM prevState { get; private set; }

    public bool reach { get; private set; }

    #endregion regoin

    private void Awake()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        myAudio = GetComponent<AudioSource>();

        if (eventValue != 0)
        {
            Event(eventValue);
        }
    }

    private void FixedUpdate()
    {        
        if (!stop) curState.Excute(this);

    }

    public void Event(int value)
    {
        if (value == 1)
        {
            curState = StakeOut();            
        }
        else if (value == 2)
        {
            curState = Student();            
        }
        curState.Enter(this);
        stop = false;
        SoundManager.Instance.PlayBGM("Ambience_Horror_Classic_03");
    }

    public void SetAudio(bool value)
    {
        if (value) myAudio.Play();
        else myAudio.Stop();
    }

    #region Setter
    public void ChangeState(FSM newState)
    {        
        curState.Exit(this);
        prevState = curState;
        curState = newState;
        curState.Enter(this);        
    }

    public void SetWalkState(bool value)
    {
        if (value) ani.SetFloat("speed", 0.8f);
        else ani.SetFloat("speed", 0);
    }

    #endregion

    #region Getter
    public float GetWalkSpeed() { return walkSpeed; }

    #endregion

    private void FootSound(int val)
    {
        if (val == 0)
        {
            SoundManager.Instance.SetSFXVolume(0.2f);

            SoundManager.Instance.PlayVFX("Walk_right");
        }
        else if (val == 1)
        {
            SoundManager.Instance.SetSFXVolume(0.13f);

            SoundManager.Instance.PlayVFX("Walk_left");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove player = collision.GetComponent<PlayerMove>();

        if (player != null)
        {
            reach = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMove player = collision.GetComponent<PlayerMove>();

        if (player != null)
        {
            reach = false;
        }
    }
}
