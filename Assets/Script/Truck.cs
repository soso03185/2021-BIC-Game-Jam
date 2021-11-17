using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public Transform playerPosition;
    float tVolume = 1;
    [SerializeField] float distance = 0.1f;
    
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
        
    }
    private void Update()
    {
        distance =this.transform.position.x - playerPosition.position.x;

        if (distance == 0) return;

        else
        {
            if (distance < 20f && distance > -70f) tVolume = 1 / Mathf.Abs(distance);
            else tVolume = 0f;
        }

        this.GetComponent<AudioSource>().volume = tVolume;

    }

    public void Event(int val)
    {
        if (val == 0) myColl.enabled = true;
        else if (val == 1) myColl.enabled = false;
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
        yield return new WaitForSecondsRealtime(0.8f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
