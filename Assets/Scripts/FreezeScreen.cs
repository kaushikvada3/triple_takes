using UnityEngine;

public class FreezeScreen : MonoBehaviour
{
    public KeyCode freezeKey = KeyCode.F;
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
        if (Input.GetKeyDown(freezeKey))
        {
            isFrozen = true;
            FreezeCharacterMovement();
        }

        if (Input.GetKeyUp(freezeKey))
        {
            isFrozen = false;
            UnfreezeCharacterMovement();
            StopDragging();
        }

        if (isFrozen)
        {
            if (Input.GetMouseButtonDown(0)) StartDragging();
            if (isDragging && Input.GetMouseButton(0)) DragObject();
            if (isDragging && Input.GetMouseButtonUp(0)) StopDragging();
        }
    }

    void FreezeCharacterMovement()
    {
        if (fpsController != null) fpsController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UnfreezeCharacterMovement()
    {
        if (fpsController != null) fpsController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void StartDragging()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var draggable = hit.collider.gameObject.GetComponent<Draggable>();
            if (draggable != null)
            {
                objectToDrag = hit.collider.gameObject;
                dragDistance = hit.distance;
                isDragging = true;
            }
        }
    }

    void StopDragging()
    {
        isDragging = false;
        objectToDrag = null;
    }

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
