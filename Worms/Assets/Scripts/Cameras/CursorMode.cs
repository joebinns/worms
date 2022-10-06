using UnityEngine;

namespace Cameras
{
    /// <summary>
    /// Simple script to change the cursor mode, for toggling between game and menus
    /// </summary>
    public static class CursorMode
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

        private static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private static void ShowCursor()
        {
            Cursor.visible = true;
        }

        private static void HideCursor()
        {
            Cursor.visible = false;
        }
    }
}
