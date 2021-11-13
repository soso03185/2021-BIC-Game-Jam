using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Homeless : MonoBehaviour
{
    public GameObject _effectShout;
    public Transform _playerPosition;
    public Sprite Right;
    public Sprite Left;
    public Sprite Forward;
    public Sprite None;
    bool _isAngry = false;
    float colorAlpha = 0f;

    Coroutine _co = null;
    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = None;
    }

    private void Update()
    {
        _effectShout.transform.position = _playerPosition.position + new Vector3(0, 2, 0);

        float playerPo = transform.position.x - _playerPosition.position.x;

        if (playerPo > 0 && playerPo < 7)
            _spriteRenderer.sprite = Left;
        else if (playerPo <= 0 && playerPo > -7)
            _spriteRenderer.sprite = Right;
        else
        {
            if (!_isAngry) _spriteRenderer.sprite = None;
        }
    }

    public void AngryHomeless()
    {
        _co = StartCoroutine("eventHomeless");

    }

    IEnumerator eventHomeless()
    {
        // Angry Homeless !!
        _isAngry = true;
        _effectShout.SetActive(true);
        _spriteRenderer.sprite = Forward;
        yield return new WaitForSeconds(1f);
        _effectShout.GetComponent<SpriteRenderer>().DOFade(0, 5.0f);
        _co = null;
    }
}
