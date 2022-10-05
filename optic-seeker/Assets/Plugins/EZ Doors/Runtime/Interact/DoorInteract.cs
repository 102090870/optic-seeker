using UnityEngine;
using UnityEngine.UI;
using EZDoor;

public class DoorInteract : MonoBehaviour
{
    public Image keyimagebackground;
    public RawImage keyimage;
    public Camera cam;
    public LayerMask layerMask;
    [Range(0.1f, 10f)] public float distance = 5.0f;

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, layerMask))
        {
            bool inRange = Vector3.Distance(transform.position, hit.transform.position) <= distance;

            if (inRange)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    IInteractable interact = hit.transform.GetComponent<IInteractable>();

                    if (interact != null)
                    {
                        if (hit.transform.tag.Equals("Interactable"))
                        {
                            keyimage.enabled = false;
                            keyimagebackground.enabled = false;
                        }
                        interact.Interact();
                    }
                }
            }
        }
    }
}
