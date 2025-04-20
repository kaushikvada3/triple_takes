using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 50f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // hold Shift to move camera
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            float moveY = 0f;

            if (Input.GetKey(KeyCode.E)) moveY = 1f;
            if (Input.GetKey(KeyCode.Q)) moveY = -1f;

            Vector3 move = new Vector3(moveX, moveY, moveZ);
            transform.Translate(move * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftAlt)) // hold Alt to rotate
        {
            float rotX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            transform.Rotate(-rotY, rotX, 0);
        }
    }
}