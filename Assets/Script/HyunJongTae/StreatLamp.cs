using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreatLamp : MonoBehaviour
{
    private GameObject lightObj;
    private GameObject shadow;

    private void Awake()
    {
        lightObj = transform.GetChild(0).gameObject;
        shadow = transform.GetChild(1).gameObject;

        shadow.SetActive(false);
    }    

    public void Event()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        int count = 0;

        while (count < 10)
        {
            if (lightObj.activeSelf) lightObj.SetActive(false);
            else lightObj.SetActive(true);
            count++;
            if (count == 10) shadow.SetActive(true);
            yield return new WaitForSeconds(0.8f);
        }
        
        yield return new WaitForSeconds(0.5f);
        lightObj.SetActive(false);
        shadow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        lightObj.SetActive(true);        
    }
}
