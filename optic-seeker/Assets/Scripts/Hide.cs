using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Camera PlayerCam;
    public Camera EnemyCam;
    public Camera thisCam;


    public GameObject hideText;
    private GameObject model;
    public PlayerMov playerMov;
    private bool OnTrigger = false;
    private GameObject Char;


    private void Awake()
    {
        Char = GameObject.Find("Mummy_char");
        thisCam.enabled = false;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && OnTrigger)
        {
            if (playerMov.isHidden)
            {
                unhide();
            }
            else
            {
                hide();
            }
            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hide")
        {
            hideText.SetActive(true);
            thisCam.transform.position=(other.gameObject.GetComponent<hideTrigger>().Prop.gameObject.transform.position + new Vector3(0,((1+ other.gameObject.GetComponent<hideTrigger>().CameraHeight) *other.gameObject.GetComponent<hideTrigger>().Prop.GetComponent<Collider>().bounds.size.y),0));
            OnTrigger = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        
        hideText.SetActive(false);
        OnTrigger = false;
        unhide();
    }

    void hide()
    {
        playerMov.isHidden = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Char.SetActive(false);
        PlayerCam.enabled = false;
        EnemyCam.enabled = false;
        thisCam.enabled = true;
        hideText.SetActive(false);
    }
    void unhide()
    {
        playerMov.isHidden = false;
        Char.SetActive(true);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        EnemyCam.enabled = false;
        thisCam.enabled = false;
        PlayerCam.enabled = true;
        hideText.SetActive(false);
    }
}
