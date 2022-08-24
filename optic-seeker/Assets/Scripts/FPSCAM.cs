using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCAM : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform Orientation;

    float xRotation;
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
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation -= Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
