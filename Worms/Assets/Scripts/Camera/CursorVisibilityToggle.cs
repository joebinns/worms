using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script to toggle cursor visibility during Play mode.
/// </summary>
public class CursorVisibilityToggle : MonoBehaviour
{
    public void EnableCursor()
    {
        Cursor.visible = true;
    }

    public void DisableCursor()
    {
        Cursor.visible = false;
    }
}
