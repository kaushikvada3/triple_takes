using UnityEngine;

public class DirectorCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public KeyCode toggleControlKey = KeyCode.C;

    private bool isControlling = false;
    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        // Toggle camera control mode
        if (Input.GetKeyDown(toggleControlKey))
        {
            isControlling = !isControlling;
            GetComponent<Camera>().enabled = isControlling;
        }

        if (!isControlling) return;

        // Mouse look (hold right click)
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * lookSpeed;
            pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float moveY = 0f;

        if (Input.GetKey(KeyCode.E)) moveY += 1f;
        if (Input.GetKey(KeyCode.Q)) moveY -= 1f;

        Vector3 direction = new Vector3(moveX, moveY, moveZ);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}