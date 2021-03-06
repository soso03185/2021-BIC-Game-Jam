using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerMove : MonoBehaviour
{
    public bool _isBack { get; set; } = true;
    public float Speed { get; set; } = 1.5f;
    public bool _isMove = true;
    public float delayEvent = 4f;

    Rigidbody2D _rigid;
    Animator _anim;
    Coroutine _co = null;

    public CharacterState _state = CharacterState.Idle;
    MoveDir _dir = MoveDir.None;
    MoveDir _lastDir = MoveDir.Right;

    public CharacterState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            _state = value;
        }
    }
    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;
            _dir = value;

            if (value != MoveDir.None)
                _lastDir = value;

            UpdateAnimation();
        }
    }
    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateController();
    }

    void UpdateController()
    {
        switch (State)
        {
            case CharacterState.Idle:
                GetInput();
                UpdateIdle();
                break;

            case CharacterState.Moving:
                GetInput();
                UpdateAnimation();
                break;

            case CharacterState.Run:
                //GetInput();
                //UpdateAnimation();
                break;
        }
    }
    void UpdateAnimation()
    {
        // Moving Animation
        if (_state == CharacterState.Moving)
        {
            switch (_dir)
            {
                case MoveDir.Left:
                    _rigid.velocity = new Vector2(Speed, _rigid.velocity.y);
                    _anim.SetFloat("Reverse", 1.0f);
                    _anim.Play("Player_Walking");
                    break;

                case MoveDir.Right:
                    _rigid.velocity = new Vector2(Speed, _rigid.velocity.y);
                    _anim.SetFloat("Reverse", 1.0f);
                    _anim.Play("Player_Walking");
                    break;

                case MoveDir.None:
                    _rigid.velocity = new Vector2(0, _rigid.velocity.y);
                    _anim.SetFloat("Reverse", 1.0f);
                    _anim.Play("Player_Idle");
                    break;
            }
        }

        // Run Animation
        //else if (_state == CharacterState.Run)
        //{
        //    switch (_dir)
        //    {
        //        case MoveDir.Left:
        //            _rigid.velocity = new Vector2(Speed, _rigid.velocity.y);
        //            _anim.Play("Player_Run");
        //            break;

        //        case MoveDir.Right:
        //            _rigid.velocity = new Vector2(Speed, _rigid.velocity.y);

        //            _anim.Play("Player_Run");
        //            break;

        //        case MoveDir.None:
        //            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        //            _anim.Play("Player_Idle");
        //            break;
        //    }
        //}
    }

    void UpdateIdle()
    {
        if (Dir != MoveDir.None)
        {
            State = CharacterState.Moving;
            return;
        }
    }

    void GetInput()
    {
        if (_isMove)
        {
            // Direction Input
            if (Input.GetAxisRaw("Horizontal") < 0 && _isBack)
                Dir = MoveDir.Left;
            else if (Input.GetAxisRaw("Horizontal") > 0)
                Dir = MoveDir.Right;
            else
                Dir = MoveDir.None;
        }
    }

    void Foot()
    {
        SoundManager.Instance.PlayVFX("Walk_right");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Event") 
        {
            Event ev = collision.GetComponent<Event>();
            delayEvent = ev.delay;
            if (delayEvent == 0) return;

            if(ev != null)
            {
                Debug.Log("player Stop");
                _co = StartCoroutine("_coPlayerStop");
            }
        }
    }

    IEnumerator _coPlayerStop()
    {
        _isMove = false;
        Dir = MoveDir.None;
        yield return new WaitForSeconds(delayEvent);

        _isMove = true;
        _co = null;
    }
}
