using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideTrigger : MonoBehaviour
{
    public Camera PlayerCam;
    public Camera EnemyCam;
    public Camera thisCam;


    public GameObject hideText;
    private GameObject player;
    private GameObject model;
    private PlayerMov playerMov;
    private bool OnTrigger = false;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMov = player.GetComponent<PlayerMov>();
        model = GameObject.Find("mummy_rig");
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
            hideText.SetActive(true);
            OnTrigger = true;
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        hideText.SetActive(false);
        OnTrigger = false;
        unhide();
    }
    void hide()
    {
        playerMov.isHidden = true;
        player.SetActive(false);
        PlayerCam.enabled = false;
        EnemyCam.enabled = false;
        thisCam.enabled = true;
        hideText.SetActive(false);
    }
    void unhide()
    {
        playerMov.isHidden = false;
        player.SetActive(true);
        EnemyCam.enabled = false;
        thisCam.enabled = false;
        PlayerCam.enabled = true;
        hideText.SetActive(false);
    }

}
