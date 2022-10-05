using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if (isPaused)
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
        pauseMenuUI.SetActive(false);
        InputManager.RevertActionMap();
        CursorMode.DisableCursor();
        Time.timeScale = 1f;
        
        isPaused = false;
    }

    public void Pause()
    {
        AudioManager.Instance.Play("Click Primary");
        pauseMenuUI.SetActive(true);
        InputManager.SwitchActionMap("Paused");
        CursorMode.EnableCursor();
        Time.timeScale = 0f;
        
        isPaused = true;
    }

    public void Exit()
    {   
        AudioManager.Instance.Play("Click Secondary");
        Time.timeScale = 1f;
        LoadingScreen.Instance.ChangeSceneImpatient(SceneIndices.MAIN_MENU);
    }
}
