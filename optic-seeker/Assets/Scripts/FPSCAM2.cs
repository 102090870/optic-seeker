using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCAM2 : MonoBehaviour
{
    public float sensX;

    public Transform Orientation;

    float yRotation;

    //Note player object must have rigidbody and set to Intepolate and Continous
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;

        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
