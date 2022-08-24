using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colourchanger : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

}
