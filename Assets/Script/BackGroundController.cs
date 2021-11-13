using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    private Transform mainCamera;

    [SerializeField] private Vector3 movementScale;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Scale(mainCamera.position, movementScale);
    }
}
