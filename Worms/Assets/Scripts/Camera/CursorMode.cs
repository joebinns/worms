using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script to change the cursor mode, for toggling between game and menus
/// </summary>
public class CursorMode : MonoBehaviour
{
    public static void EnableCursor()
    {
        ShowCursor();
        UnlockCursor();
    }

    public static void DisableCursor()
    {
        HideCursor();
        LockCursor();
    }
    
    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void ShowCursor()
    {
        Cursor.visible = true;
    }

    public static void HideCursor()
    {
        Cursor.visible = false;
    }
}
