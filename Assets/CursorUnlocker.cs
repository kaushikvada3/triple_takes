using UnityEngine;

public class CursorUnlocker : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlocks the cursor
        Cursor.visible = true;                   // Shows the cursor
    }
}