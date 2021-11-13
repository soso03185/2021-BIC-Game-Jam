using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    private Collider2D myColl;
    [SerializeField] private float speed;

    [SerializeField] private bool start;

    private GameObject black;


    private void Awake()
    {
        myColl = GetComponent<Collider2D>();
        black = transform.GetChild(0).GetChild(0).gameObject;

    }

    private void Start()
    {
        black.SetActive(false);
        if (start) Event();
    }

    private void Event()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        while (true)            
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove player = collision.GetComponent<PlayerMove>();

        if (player != null)
        {
            Dead();
        }
    }

    private void Dead()
    {
        myColl.enabled = false;
        StartCoroutine(RoutineDead());
    }

    private IEnumerator RoutineDead()
    {
        black.SetActive(true);
        SoundManager.Instance.PlayVFX("horn");
        yield return new WaitForSecondsRealtime(0.03f);
        SoundManager.Instance.PlayVFX("brake");
        yield return new WaitForSecondsRealtime(0.2f);
        SoundManager.Instance.PlayVFX("hit");
    }
}
