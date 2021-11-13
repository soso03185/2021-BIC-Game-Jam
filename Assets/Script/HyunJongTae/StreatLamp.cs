using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreatLamp : MonoBehaviour
{
    private AudioSource myAudio;

    private GameObject lightObj;
    private GameObject shadow;
    private GameObject catObj;

    private Transform player;

    [SerializeField] private bool start;
    [SerializeField] private float checkDis = 5;

    private void Awake()
    {
        lightObj = transform.GetChild(0).gameObject;
        shadow = transform.GetChild(1).gameObject;
        catObj = transform.GetChild(2).gameObject;
        myAudio = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        shadow.SetActive(false);
        catObj.SetActive(false);

        if (start) Event();
    }    

    public void Event()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        int count = 0;
        myAudio.Play();
    
        while (count < 13)
        {
            if (lightObj.activeSelf) lightObj.SetActive(false);
            else lightObj.SetActive(true);
            count++;            
            yield return new WaitForSeconds(0.8f - count * 0.04f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        shadow.SetActive(true);
        lightObj.SetActive(true);
        float distance = 100;
        while (distance > checkDis)
        {
            distance = Vector3.Distance(player.position, transform.position);           
            yield return new WaitForFixedUpdate();
        }
        myAudio.Stop();
        SoundManager.Instance.SetSFXVolume(1.0f);
        SoundManager.Instance.PlayVFX("Spark");
        lightObj.SetActive(false);
        shadow.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        lightObj.SetActive(true);
        catObj.SetActive(true);

        yield return new WaitForSecondsRealtime(0.8f);
        SoundManager.Instance.PlayVFX("Catmeow6");
    }
}
