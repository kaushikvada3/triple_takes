using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera directorCamera;
    public KeyCode switchKey = KeyCode.V;

    void Start()
    {
        if (mainCamera != null && directorCamera != null)
        {
            mainCamera.enabled = true;
            directorCamera.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            bool isMainActive = mainCamera.enabled;

            mainCamera.enabled = !isMainActive;
            directorCamera.enabled = isMainActive;
        }
    }
}