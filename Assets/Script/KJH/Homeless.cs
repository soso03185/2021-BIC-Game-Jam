using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homeless : MonoBehaviour
{
    public Transform _playerPosition;
    public Sprite Right;
    public Sprite Left;
    public Sprite Forward;
    public Sprite None;

    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = None;
    }

    private void Update()
    {
        float playerPo = transform.position.x - _playerPosition.position.x;

        if (playerPo > 0 && playerPo < 7) // HomeLess is Left
        {
            _spriteRenderer.sprite = Left;
        }
        else if (playerPo <= 0 && playerPo > -7) // HomeLess is Right
        {
            _spriteRenderer.sprite = Right;
        }
        else
            _spriteRenderer.sprite = None;        
    }

    void eventHomeless()
    {
        // Angry Homeless !!
        _spriteRenderer.sprite = Forward;
    }
}
