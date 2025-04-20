using UnityEngine;

public class FreezeScreen : MonoBehaviour
{
    public KeyCode freezeKey = KeyCode.F; // Key to hold for freezing the screen
    private bool isFrozen = false;
    private bool isDragging = false;
    private float dragDistance;
    private GameObject objectToDrag;
    private Camera mainCamera;

    private SimpleFPSController fpsController;

    void Start()
    {
        fpsController = Camera.main.GetComponentInParent<SimpleFPSController>();
        if (fpsController == null)
        {
            Debug.LogWarning("No SimpleFPSController found on Main Camera's parent!");
        }

        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if the freeze key is being pressed down
        if (Input.GetKeyDown(freezeKey))
        {
            isFrozen = true;
            FreezeCharacterMovement();
        }
        
        // Check if the freeze key is released
        if (Input.GetKeyUp(freezeKey))
        {
            isFrozen = false;
            UnfreezeCharacterMovement();
            StopDragging();
        }

        // Only handle dragging functionality when the screen is frozen
        if (isFrozen)
        {
            // Check for initial mouse click to start dragging
            if (Input.GetMouseButtonDown(0))
            {
                StartDragging();
            }
            
            // Continue dragging while mouse button is held
            if (isDragging && Input.GetMouseButton(0))
            {
                DragObject();
            }
            
            // Stop dragging when mouse button is released
            if (isDragging && Input.GetMouseButtonUp(0))
            {
                StopDragging();
            }
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

    // Start dragging the object
    void StartDragging()
    {
        // Raycast to select the object to drag
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Draggable"))
            {
                objectToDrag = hit.collider.gameObject;
                dragDistance = hit.distance; // Store the hit distance for depth
                isDragging = true;
            }
        }
    }

    // Stop dragging the object
    void StopDragging()
    {
        isDragging = false;
        objectToDrag = null; // Clear the reference to the dragged object
    }

    // Handle object dragging
    void DragObject()
    {
        if (objectToDrag != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 newPosition = ray.origin + ray.direction * dragDistance;
            objectToDrag.transform.position = newPosition;
        }
    }
}