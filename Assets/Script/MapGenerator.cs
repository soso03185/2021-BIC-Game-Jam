using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int mapSize;

    [SerializeField] private GameObject[] mapObj;
    private int percent;

    private void Start()
    {
        //percent = 100 / mapObj.Length;

        for (int i = 0; i < mapSize; i++)
        {
            int rand = Random.Range(0, 100);
            

        }
    }    

}
