using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public Transform cameraPos;

    private void Start()
    {
        transform.position = cameraPos.position;
    }
    private void Update()
    {
        transform.position = cameraPos.position;
    }
}
