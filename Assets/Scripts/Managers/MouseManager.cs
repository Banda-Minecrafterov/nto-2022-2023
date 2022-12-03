using UnityEngine;

public class MouseManager
{
    public static void SetMouseMode(bool isVisible)
    {
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible   = isVisible;
    }
}
