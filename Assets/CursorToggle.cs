using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    private bool isEditing = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))  // Press Tab to toggle mode
        {
            isEditing = !isEditing;

            if (isEditing)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}