using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float lookSpeed = 2f;
    public float scrollSpeed = 10f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        // Mouse Look
        if (Input.GetMouseButton(1)) // Right-click held
        {
            yaw += lookSpeed * Input.GetAxis("Mouse X");
            pitch -= lookSpeed * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        // WASD Movement
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        // Scroll Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * scroll * scrollSpeed, Space.Self);
    }
}