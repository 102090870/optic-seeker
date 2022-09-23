using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image knifeimagebackground;
    public RawImage knifeimage;
    public Image fuelimagebackground;
    public RawImage fuelimage;
    public Text totalnumber;
    void Start()
    {
        totalnumber.enabled = false;
        knifeimage.enabled = false;
        fuelimage.enabled = false;
        knifeimagebackground.enabled = false;
        fuelimagebackground.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
