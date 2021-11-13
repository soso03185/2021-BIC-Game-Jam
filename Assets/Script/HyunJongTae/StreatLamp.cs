using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreatLamp : MonoBehaviour
{
    private GameObject lightObj;
    private GameObject shadow;
    private GameObject catObj;

    [SerializeField] private bool start;

    private void Awake()
    {
        lightObj = transform.GetChild(0).gameObject;
        shadow = transform.GetChild(1).gameObject;
        catObj = transform.GetChild(2).gameObject;

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

        while (count < 14)
        {
            if (lightObj.activeSelf) lightObj.SetActive(false);
            else lightObj.SetActive(true);
            count++;
            if (count == 14) shadow.SetActive(true);
            yield return new WaitForSeconds(0.8f - count * 0.04f);
        }
        
        yield return new WaitForSeconds(0.5f);
        lightObj.SetActive(false);
        shadow.SetActive(false);
        yield return new WaitForSeconds(2f);
        lightObj.SetActive(true);
        catObj.SetActive(true);
    }
}
