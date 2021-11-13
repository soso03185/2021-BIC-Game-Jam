using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemt : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 startPos;


    private void Start()
    {
        startPos = transform.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 pos = new Vector3(target.position.x, startPos.y, startPos.z);
        if (target.position.x < startPos.x) pos.x = startPos.x;        

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 2);
    }
}
