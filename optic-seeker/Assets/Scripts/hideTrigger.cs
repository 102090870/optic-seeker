using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideTrigger : MonoBehaviour
{
    public GameObject hideText;
    public GameObject player;
    private PlayerMov playerMov;
    public static bool isHidden = false;
    private void Awake()
    {
         playerMov = player.GetComponent<PlayerMov>();
    }
    void Update()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        hideText.SetActive(true);
    }
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHidden)
            {
                unhide();
            }
            else
            {
                hide();

            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        hideText.SetActive(false);
        unhide();
    }
    void hide()
    {
        hideText.SetActive(false);
        playerMov.isHidden = true;
    }
    void unhide()
    {
        playerMov.isHidden = false;
    }

}
