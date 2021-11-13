using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSound : MonoBehaviour
{    
    private AudioSource myAudio;
    private bool isPlaying;

    private Transform target;


    private void Awake()
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isPlaying) return;

        float distance = Vector2.Distance(transform.position, target.position);
        myAudio.volume = 1 - (distance / 6) + 0.1f;
        myAudio.pitch = 1.8f - (distance / 6);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Danger") && !isPlaying)
        {
            myAudio.Play();
            isPlaying = true;
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Danger") && isPlaying)
        {
            myAudio.Stop();
            isPlaying = false;
            target = null;
        }
    }
}
