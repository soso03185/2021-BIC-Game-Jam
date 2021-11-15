using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField] private GameObject eventObj;

    private void Awake()
    {
        if (Stage.StageCount == 4)
        {
            Ending();
        }
    }

    private void Ending()
    {
        eventObj.SetActive(true);
    }

}
