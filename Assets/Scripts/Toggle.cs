using UnityEngine;

public class Toggle : MonoBehaviour
{
    public GameObject targetAsset;
    public KeyCode toggleKey;
    public bool initState;
    public float distanceFromCamera = 2f;

    private SimpleFPSController fpsController;
    private bool wasEnabled;
    private Camera mainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetAsset.SetActive(initState);
        mainCamera = Camera.main;
        fpsController = Camera.main.GetComponentInParent<SimpleFPSController>();
        if (fpsController == null)
        {
            Debug.LogWarning("No SimpleFPSController found on Main Camera's parent!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            targetAsset.SetActive(!targetAsset.activeSelf);

            if (fpsController != null)
            {
                if (targetAsset.activeSelf)
                {
                    // store current state and disable camera movement
                    wasEnabled = fpsController.enabled;
                    fpsController.enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    // restore previous state
                    fpsController.enabled = wasEnabled;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        if (targetAsset.activeSelf)
        {
            targetAsset.transform.position = mainCamera.transform.position + 
                                             mainCamera.transform.forward * distanceFromCamera;
            targetAsset.transform.rotation = mainCamera.transform.rotation;
        }
    }
}