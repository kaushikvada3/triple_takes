using UnityEngine;

public class SimpleCameraSwitcher : MonoBehaviour
{
    [Header("Cameras to Toggle Between")]
    public Camera mainCamera;
    public Camera directorCamera;
    public Camera shotCamera1;

    [Header("Toggle Key")]
    public KeyCode switchKey = KeyCode.V;

    private int camIndex = 0;

    void Start()
    {
        EnableOnly(mainCamera); // Start with main camera
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            camIndex = (camIndex + 1) % 3;

            switch (camIndex)
            {
                case 0:
                    EnableOnly(mainCamera);
                    Debug.Log("Switched to Main Camera");
                    break;
                case 1:
                    EnableOnly(directorCamera);
                    Debug.Log("Switched to Director Camera");
                    break;
                case 2:
                    EnableOnly(shotCamera1);
                    Debug.Log("Switched to Shot Camera 1");
                    break;
            }
        }
    }

    void EnableOnly(Camera cam)
    {
        if (mainCamera != null) mainCamera.enabled = false;
        if (directorCamera != null) directorCamera.enabled = false;
        if (shotCamera1 != null) shotCamera1.enabled = false;

        if (cam != null) cam.enabled = true;
    }
}