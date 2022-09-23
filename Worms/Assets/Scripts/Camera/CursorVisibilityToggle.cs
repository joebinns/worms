using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script to toggle cursor visibility during Play mode.
/// </summary>
public class CursorVisibilityToggle : MonoBehaviour
{
    public static void EnableCursor()
    {
        Cursor.visible = true;
    }

    public static void DisableCursor()
    {
        Cursor.visible = false;
    }
}
