using EZDoor;
using UnityEngine;
using UnityEngine.UI;

public class PickupKey : MonoBehaviour, IInteractable
{
    public Image keyimagebackground;
    public RawImage keyimage;
    public Key key;
    public string playerTag;
    private KeyContainer keyContainer;
    public RawImage detect;

    private void Awake()
    {
        keyContainer = GameObject.FindWithTag("Player").GetComponent<KeyContainer>();
    }

    public void Pickup()
    {
        detect.enabled = false;
        keyimage.enabled = true;
        keyimagebackground.enabled = true;
        keyContainer.keys.Add(key);
        Destroy(gameObject);
    }

    public void Interact()
    {
        //Pickup();
    }
}
