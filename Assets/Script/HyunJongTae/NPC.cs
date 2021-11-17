using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator ani;
    public PlayerMove player { get; private set; }
    private AudioSource myAudio;
    public AudioSource Heartbeataudio;

    [SerializeField] private float walkSpeed;
    [SerializeField] private int eventValue;

    private bool stop = true;

    private float time;
    private float checkTime = 8;

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

    [SerializeField] private GameObject black;

    public void Ending()
    {
        Heartbeataudio.gameObject.SetActive(false);
        black.SetActive(true);
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        SoundManager.Instance.PlayVFX("Female screams 3");
        yield return new WaitForSecondsRealtime(4.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(7);
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

    public void SetWalkSpeed(float value)
    {
        walkSpeed *= value;
    }

    #endregion

    #region Getter
    public float GetWalkSpeed() { return walkSpeed; }

    #endregion

    private void FootSound(int val)
    {
        SoundManager.Instance.SetSFXVolume(0.5f);

        SoundManager.Instance.PlayVFX("Walk_left");
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
