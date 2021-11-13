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
        int rand = 0;
        int history = 0;
        for (int i = 1; i < mapSize; i++)
        {
            rand = Random.Range(0, mapObj.Length);
            if (mapObj.Length != 1)
            {
                while (rand != history)
                {
                    rand = Random.Range(0, mapObj.Length);
                }
                history = rand;
            }    

            Instantiate(mapObj[rand], new Vector2(19.2f * i, 0), Quaternion.identity, transform);
        }
    }    

}
