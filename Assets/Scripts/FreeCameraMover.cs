using UnityEngine;

public class FreeCameraMover : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isEditing = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Vector3 rot = transform.localRotation.eulerAngles;
        rotationX = rot.y;
        rotationY = rot.x;

        // 🔊 FIX: Remove extra AudioListener if one already exists
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        if (listeners.Length > 1)
        {
            foreach (AudioListener listener in listeners)
            {
                if (listener.gameObject != this.gameObject)
                {
                    Destroy(listener);  // remove extra
                }
            }
        }
    }

    void Update()
    {
        // Toggle edit/fps mode with L
        if (Input.GetKeyDown(KeyCode.L))
        {
            isEditing = !isEditing;
            Cursor.lockState = isEditing ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isEditing;
            Debug.Log(isEditing ? "🖱️ Edit Mode" : "🎮 FPS Mode");
        }

        // Only rotate when right-clicking
        if (Input.GetMouseButton(1))
        {
            rotationX += Input.GetAxis("Mouse X") * lookSpeed;
            rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationY = Mathf.Clamp(rotationY, -90f, 90f);
            transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
        }

        // WASD + E/Q movement
        float moveX = Input.GetAxis("Horizontal");   // A/D
        float moveZ = Input.GetAxis("Vertical");     // W/S
        float moveY = 0f;

        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space)) moveY = 1f;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftControl)) moveY = -1f;

        Vector3 move = new Vector3(moveX, moveY, moveZ);

        // Translate relative to world for vertical, and camera for XZ
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 horizontalMove = (right * moveX + forward * moveZ);
        Vector3 verticalMove = Vector3.up * moveY;

        transform.position += (horizontalMove + verticalMove) * moveSpeed * Time.deltaTime;
    }
}