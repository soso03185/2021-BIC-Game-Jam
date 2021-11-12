using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 10f;
    bool _isRun = false;
    Rigidbody2D _rigid;
    Animator _anim;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rigid.velocity = new Vector2(-Speed, _rigid.velocity.y);

            if (_isRun)
            {
                //_anim.SetBool(isWalk, false);
                //_anim.SetBool(isRun, true);
            }
            else
            {
                //_anim.SetBool(isWalk, true);
                //_anim.SetBool(isRun, false);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rigid.velocity = new Vector2(Speed, _rigid.velocity.y);
 //           _anim.SetBool( , true);
        }
 //       else
 //           _anim.SetBool( , false);

    }
}
