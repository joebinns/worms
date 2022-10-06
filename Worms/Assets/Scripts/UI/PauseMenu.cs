using Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using CursorMode = Camera.CursorMode;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        private bool _isPaused = false;

        public GameObject PauseMenuUI;

        public void TogglePause(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Resume()
        {
            AudioManager.Instance.Play("Click Primary");
            PauseMenuUI.SetActive(false);
            InputManager.RevertActionMap();
            CursorMode.DisableCursor();
            Time.timeScale = 1f;
        
            _isPaused = false;
        }

        public void Pause()
        {
            AudioManager.Instance.Play("Click Primary");
            PauseMenuUI.SetActive(true);
            InputManager.SwitchActionMap("Paused");
            CursorMode.EnableCursor();
            Time.timeScale = 0f;
        
            _isPaused = true;
        }

        public void Exit()
        {   
            AudioManager.Instance.Play("Click Secondary");
            Time.timeScale = 1f;
            LoadingScreen.Instance.ChangeSceneImpatient(SceneIndices.MAIN_MENU);
        }
    }
}
