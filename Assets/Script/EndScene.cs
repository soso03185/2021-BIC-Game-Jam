using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField] private GameObject eventObj;

    private void OnEnable()
    {
        if (Stage.StageCount >= 4)
        {
            Debug.Log("last event activated.");
            Ending();
        }
    }

    private void Ending()
    {
        eventObj.SetActive(true);
    }

}
