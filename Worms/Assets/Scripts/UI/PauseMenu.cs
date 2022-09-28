using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    private void OnEnable()
    {
        LoadingScreen.OnTransitionedToLoadingScreen += BehindTheCurtain;
    }

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
        pauseMenuUI.SetActive(false);
        InputManager.RevertActionMap();
        if (CameraManager.state == CameraState.AimCamera)
        {
            CursorVisibilityToggle.DisableCursor();
            Cursor.lockState = CursorLockMode.Locked;
        }
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        InputManager.SwitchActionMap("Paused");
        CursorVisibilityToggle.EnableCursor();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Exit()
    {   
        Time.timeScale = 1f;
        StartCoroutine(LoadingScreen.TransitionToLoadingScreen());
    }

    private void BehindTheCurtain()
    {
        // Remove PlayerManager and Players from DontDestroyOnLoad
        //Destroy(FindObjectOfType<PlayerManager>().gameObject);

        // Load scene
        LoadingScreen.LoadScene(SceneIndices.MAIN_MENU);
        LoadingScreen.TransitionFromLoadingScreen();
    }
}
