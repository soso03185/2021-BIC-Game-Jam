using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Homeless : MonoBehaviour
{
    [SerializeField] UnityEngine.Rendering.Universal.UniversalAdditionalCameraData volumeCam;
    public Text _shoutText;
    public GameObject _effectShout;
    public Transform _playerPosition;
    public Sprite Right;
    public Sprite Left;
    public Sprite Forward;
    public Sprite None;
    bool _isAngry = false;

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
        _shoutText.fontSize = 300;
        SoundManager.Instance.PlayVFX("Homeless");
        yield return new WaitForSeconds(0.1f);

        volumeCam.SetRenderer(1);
        _effectShout.GetComponent<SpriteRenderer>().DOFade(0, 10.0f);
        _shoutText.fontSize = 100;
        yield return new WaitForSeconds(5f);

        volumeCam.SetRenderer(0);
        _co = null;
    }
}
