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

    private void Awake()
    {
        keyContainer = GameObject.FindWithTag(playerTag).GetComponent<KeyContainer>();
    }

    public void Pickup()
    {
        
        keyimage.enabled = true;
        keyimagebackground.enabled = true;
        keyContainer.keys.Add(key);
        Destroy(gameObject);
    }

    public void Interact()
    {
        Pickup();
    }
}
