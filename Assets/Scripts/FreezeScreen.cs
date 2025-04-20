using UnityEngine;

public class FreezeScreen : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.F; // Key to toggle the freeze screen
    private bool isFrozen = false;
    private SimpleFPSController fpsController;
    private GameObject objectToDrag;
    private bool isDragging = false;
    private Vector3 offset;

    private Camera mainCamera;
    private Vector3 originalCameraRotation;

    void Start()
    {
        fpsController = Camera.main.GetComponentInParent<SimpleFPSController>();
        mainCamera = Camera.main;
        originalCameraRotation = mainCamera.transform.eulerAngles;

        if (fpsController == null)
        {
            Debug.LogWarning("No SimpleFPSController found on Main Camera's parent!");
        }
    }

    void Update()
    {
        // When the toggle key is pressed, freeze the screen and start dragging the object
        if (Input.GetKeyDown(toggleKey) && !isFrozen)
        {
            isFrozen = true;
            FreezeCharacterMovement();
            StartDragging();
        }

        // Continue dragging the object if frozen
        if (isFrozen && isDragging)
        {
            DragObject();
        }

        // If the key is released, unfreeze and reset camera
        if (Input.GetKeyUp(toggleKey) && isFrozen)
        {
            StopDragging();
            UnfreezeCharacterMovement();
            ChangeCameraPerspective(); // Change the camera perspective on release
            isFrozen = false;
        }
    }

    // Freeze character movement
    void FreezeCharacterMovement()
    {
        if (fpsController != null)
        {
            fpsController.enabled = false; // Disable FPS controller
        }
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    // Unfreeze character movement
    void UnfreezeCharacterMovement()
    {
        if (fpsController != null)
        {
            fpsController.enabled = true; // Enable FPS controller
        }
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor again
        Cursor.visible = false; // Hide the cursor
    }

    // Change the camera's perspective (you can modify this to fit your needs)
    void ChangeCameraPerspective()
    {
        // For example, let's modify the camera's rotation
        mainCamera.transform.eulerAngles = new Vector3(originalCameraRotation.x, originalCameraRotation.y + 90f, originalCameraRotation.z); // Adjust the camera by 90 degrees on Y-axis
    }

    // Start dragging the object
    private void StartDragging()
    {
        // Raycast to select the object to drag
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Draggable"))
            {
                objectToDrag = hit.collider.gameObject;
                offset = objectToDrag.transform.position - ray.origin;
                isDragging = true;
            }
        }
    }

    // Stop dragging the object
    private void StopDragging()
    {
        isDragging = false;
        objectToDrag = null; // Clear the reference to the dragged object
    }

    // Handle object dragging
    private void DragObject()
    {
        if (objectToDrag != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPos = ray.origin + ray.direction * offset.z; // Adjust for depth
            objectToDrag.transform.position = worldPos;
        }
    }
}
