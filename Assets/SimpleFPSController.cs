using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFPSController : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float lookSpeed = 2f;
    float upDownRot = 0f;
    public float maxLookAngle = 85f;

    CharacterController cc;
    Camera cam;

    void Start() {
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // Mouse look
        float mx = Input.GetAxis("Mouse X") * lookSpeed;
        transform.Rotate(0, mx, 0);

        upDownRot -= Input.GetAxis("Mouse Y") * lookSpeed;
        upDownRot = Mathf.Clamp(upDownRot, -maxLookAngle, maxLookAngle);
        cam.transform.localRotation = Quaternion.Euler(upDownRot, 0, 0);

        // Movement
        Vector3 move = (transform.forward * Input.GetAxis("Vertical")
                      + transform.right   * Input.GetAxis("Horizontal"))
                      * walkSpeed;
        cc.SimpleMove(move);
    }
}