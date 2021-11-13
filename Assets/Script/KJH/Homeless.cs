using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homeless : MonoBehaviour
{
    public GameObject _effectShout;
    public Transform _playerPosition;
    public Sprite Right;
    public Sprite Left;
    public Sprite Forward;
    public Sprite None;
    bool _isAngry = false;
    float colorAlpha = 1f;

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
        _effectShout.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, colorAlpha);

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

    void AngryHomeless()
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

        colorAlpha -= Time.deltaTime * 0.1f;
        _co = null;

        yield return new WaitForSeconds(3f);
    }
}
