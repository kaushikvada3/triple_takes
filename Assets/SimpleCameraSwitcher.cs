using UnityEngine;

public class SimpleCameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera directorCamera;
    public KeyCode switchKey = KeyCode.V;

    void Start()
    {
        mainCamera.enabled = true;
        directorCamera.enabled = false;
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