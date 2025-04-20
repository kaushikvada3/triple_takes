using UnityEngine;

public class DraggableCamera : MonoBehaviour
{
    private bool isDragging = false;
    private float dragSpeed = 10f;
    private Vector3 offset;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                isDragging = true;
                offset = transform.position - hit.point;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 newPos = hit.point + offset;
                newPos.y = transform.position.y; // Keep height constant
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * dragSpeed);
            }
        }
    }
}