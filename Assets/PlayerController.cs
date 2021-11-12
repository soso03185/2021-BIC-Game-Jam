using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float Speed;

    [SerializeField]
    float JumpPower;

    public float dashSpeed;
    public float startDashTime;
    
    [SerializeField]
    bool _isJump = true;
    bool _isDash = true;
    bool _isDead = false;
    [SerializeField]
    int _wallJumpCount = 0;
    int MaxwallJumpCount = 4;

    [SerializeField]
    bool _isWallJump = false;

    SpriteRenderer _sprite;
    Rigidbody2D _rigid;
    Animator _anim;
    Coroutine _coSkill;

    // Define State
    public CharacterState _state = CharacterState.Idle;
    MoveDir _dir = MoveDir.None;
    MoveDir _lastDir = MoveDir.Right;

    [SerializeField]
    Vector3 dirVec = Vector3.right;

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

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (_isDead)
            return;

        _sprite.color = new Color(1f, 1f - 0.15f * _wallJumpCount, 1f - 0.15f * _wallJumpCount);
        UpdateController();
    }

    private void FixedUpdate()
    {
        // Ray
        Debug.DrawRay(_rigid.position, dirVec * 0.75f, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, dirVec, 0.75f, LayerMask.GetMask("Block"));

        if (rayHit.collider != null)
        {
            Speed = 0.0f;
            _isWallJump = true;

            if (_rigid.velocity.y < 0)
                _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y * 0.4f);
            else if (_rigid.velocity.y == 0) 
                _isWallJump = false;
        }

        else 
        {
            _isWallJump = false;
            Speed = 7.0f;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Floor Touch 
        var normal = collision.contacts[0].normal;
        normal.x = Mathf.Abs(normal.x);
        if (Vector2.Dot(normal, Vector2.up) >= 0.7f)
        {
            _isJump = true;
            _wallJumpCount = 0;
        }
    }
    void UpdateController()
    {
        switch (State)
        {
            case CharacterState.Idle:
                GetInput();
                UpdateIdle();
                WallJump(); 
                break;

            case CharacterState.Moving:
                GetInput();
                UpdateAnimation();
                WallJump();
                break;

            case CharacterState.Jump:
                GetInput();
                WallJump();
                break;

            case CharacterState.Dash:
                break;

            case CharacterState.WallJump:
                break;

            case CharacterState.Dead:
                UpdateDead();
                break;
        }
    }

    // Idle State
    void UpdateIdle()
    {
        if (Dir != MoveDir.None)
        {
            State = CharacterState.Moving;
            return;
        }
    }

    // WallJump State
    void WallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isWallJump)
        {
            State = CharacterState.WallJump;

            if (_wallJumpCount < MaxwallJumpCount)
                _coSkill = StartCoroutine("CoStartWallJump");
            else
                State = CharacterState.Idle;
        }
    }

    // Dead State
    void UpdateDead()
    {
        Debug.Log("Dead !");
        _isDead = true;
        StopAllCoroutines();
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
    }

    // Animation Code
    void UpdateAnimation()
    {
        // Moving Animation
        if (_state == CharacterState.Moving)
        {
            switch (_dir)
            {
                case MoveDir.Left:
                    _rigid.velocity = new Vector2(-Speed, _rigid.velocity.y);
                    dirVec = Vector3.left;

                    _sprite.flipX = false;
                    //_anim.Play("??????? ???");
                    break;

                case MoveDir.Right:
                    _rigid.velocity = new Vector2(Speed, _rigid.velocity.y);
                    dirVec = Vector3.right;

                    _sprite.flipX = true;
                    //_anim.Play("??????? ???");
                    break;

                case MoveDir.None:
                    _rigid.velocity = new Vector2(0, _rigid.velocity.y);
                    //_anim.Play("??????? ???");
                    break;
            }
        }

        // Dash Animation
        else if (_state == CharacterState.Dash)
        {
            switch (_lastDir)
            {
                case MoveDir.Left:
                    //_anim.Play("??????? ???");
                    break;

                case MoveDir.Right:
                   // _anim.Play("??????? ???");
                    break;
            }
        }

        // Jump Animation
        else if (_state == CharacterState.Jump)
        {
            switch (_lastDir)
            {
                case MoveDir.Left:
                    //_anim.Play("??????? ???");
                    break;

                case MoveDir.Right:
                    //_anim.Play("??????? ???");
                    break;
            }
        }
    }

    void GetInput()
    {
        // Direction Input
        if (Input.GetKey(KeyCode.A))
            Dir = MoveDir.Left;
        else if (Input.GetKey(KeyCode.D))
            Dir = MoveDir.Right;
        else
            Dir = MoveDir.None;

        // Dash Input
        if (Input.GetKeyDown(KeyCode.E) && _isDash)
        {
            State = CharacterState.Dash;
            _coSkill = StartCoroutine("CoStartDash");
        }

        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && _isJump)
        {
            State = CharacterState.Jump;
            _coSkill = StartCoroutine("CoStartJump");
        }
    }

    // Dash Excute
    IEnumerator CoStartDash()
    {
        Debug.Log("CoStartDash !");
        //SoundManager.Instance.PlayVFX("Dash");

        _isDash = false;
        _rigid.velocity = Vector2.one * dirVec * dashSpeed;

        yield return new WaitForSeconds(startDashTime);
        _rigid.velocity = Vector2.zero;
        State = CharacterState.Idle;
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);

        yield return new WaitForSeconds(startDashTime);
        _coSkill = null;
        _isDash = true;
    }

    // Jump Excute
    IEnumerator CoStartJump()
    {
        Debug.Log("CoStartJump !");
        // SoundManager.Instance.PlayVFX("Jump");
        _rigid.velocity = new Vector2(_rigid.velocity.x , JumpPower);
        _isJump = false;

        yield return new WaitForSeconds(0.1f);
        State = CharacterState.Idle;
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);

        yield return new WaitForSeconds(0.5f);
        _coSkill = null;
    }

    // WallJump Excute
    IEnumerator CoStartWallJump()
    {
        float wallSidePower = JumpPower;

        if (dirVec == Vector3.left) wallSidePower *= 1;
        else if (dirVec == Vector3.right) wallSidePower *= -1;
        _rigid.velocity = new Vector2(wallSidePower * 0.45f, JumpPower);


        dirVec = -dirVec;
        Debug.Log("CoStartWallJump !");
        _coSkill = null;
        yield return new WaitForSeconds(0.1f);

        State = CharacterState.Idle;
        _isWallJump = false;

        if(_wallJumpCount < MaxwallJumpCount)
            _wallJumpCount++;
    }
}
