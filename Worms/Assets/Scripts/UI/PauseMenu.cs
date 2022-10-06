using Audio;
using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuUI;
        private bool _isPaused = false;

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
            _pauseMenuUI.SetActive(false);
            InputManager.Instance.RevertActionMap();
            Cameras.CursorMode.DisableCursor();
            Time.timeScale = 1f;
        
            _isPaused = false;
        }

        public void Pause()
        {
            AudioManager.Instance.Play("Click Primary");
            _pauseMenuUI.SetActive(true);
            InputManager.Instance.SwitchActionMap("Paused");
            Cameras.CursorMode.EnableCursor();
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
