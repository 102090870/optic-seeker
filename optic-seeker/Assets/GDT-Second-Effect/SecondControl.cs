using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondControl : MonoBehaviour
{

    public GameObject secondEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            secondEffect.SetActive(true);
        }
    }

    //public void startScript()
    //{
    //    fadeEffect.SetActive(true);
    //}
}
