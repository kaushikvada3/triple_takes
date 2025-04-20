using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private bool dragging = false;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        dragging = true;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);  
        Vector3 mousePoint = Input.mousePosition; 
        mousePoint.z = screenPoint.z;
        offset = transform.position - cam.ScreenToWorldPoint(mousePoint); 
    }

    void OnMouseDrag() //
    {
        if (dragging) 
        {
            Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = screenPoint.z;
            transform.position = cam.ScreenToWorldPoint(mousePoint) + offset;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
    }
    

    
}
